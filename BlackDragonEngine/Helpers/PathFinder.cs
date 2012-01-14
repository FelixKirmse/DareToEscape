using System.Collections.Generic;
using System.Linq;
using BlackDragonEngine.TileEngine;
using Microsoft.Xna.Framework;

namespace BlackDragonEngine.Helpers
{
    public static class PathFinder<TMap, TCodes> where TMap : IMap<TCodes>, new()
    {
        #region Declarations

        public const int CostStraight = 10;
        public const int CostDiagonal = 15;
        private static readonly Dictionary<Vector2, NodeStatus> nodeStatus = new Dictionary<Vector2, NodeStatus>();

        private static readonly List<PathNode<TMap, TCodes>> openList = new List<PathNode<TMap, TCodes>>();

        private static readonly Dictionary<Vector2, float> nodeCosts = new Dictionary<Vector2, float>();

        private enum NodeStatus
        {
            Open,
            Closed
        };

        #endregion

        #region Helper Methods

        private static void AddNodeToOpenList(PathNode<TMap, TCodes> node)
        {
            int index = 0;
            float cost = node.TotalCost;

            while ((openList.Count() > index) && (cost < openList[index].TotalCost))
            {
                ++index;
            }
            openList.Insert(index, node);
            nodeCosts[node.GridLocation] = node.TotalCost;
            nodeStatus[node.GridLocation] = NodeStatus.Open;
        }

        private static List<PathNode<TMap, TCodes>> FindAdjacentNodes(PathNode<TMap, TCodes> currentNode,
                                                                      PathNode<TMap, TCodes> endNode,
                                                                      TileMap<TMap, TCodes> tileMap)
        {
            var adjacentNodes = new List<PathNode<TMap, TCodes>>();

            int X = currentNode.GridX;
            int Y = currentNode.GridY;

            bool upLeft = true;
            bool upRight = true;
            bool downLeft = true;
            bool downRight = true;

            if ((X > 0) && (tileMap.CellIsPassable(X - 1, Y)))
            {
                adjacentNodes.Add(new PathNode<TMap, TCodes>(currentNode, endNode, new Vector2(X - 1, Y),
                                                             CostStraight + currentNode.DirectCost, tileMap));
            }
            else
            {
                upLeft = false;
                downLeft = false;
            }

            if ((X < 49) && (tileMap.CellIsPassable(X + 1, Y)))
            {
                adjacentNodes.Add(new PathNode<TMap, TCodes>(currentNode, endNode, new Vector2(X + 1, Y),
                                                             CostStraight + currentNode.DirectCost, tileMap));
            }
            else
            {
                upRight = false;
                downRight = false;
            }

            if ((Y > 0) && (tileMap.CellIsPassable(X, Y - 1)))
            {
                adjacentNodes.Add(new PathNode<TMap, TCodes>(currentNode, endNode, new Vector2(X, Y - 1),
                                                             CostStraight + currentNode.DirectCost, tileMap));
            }
            else
            {
                upLeft = false;
                upRight = false;
            }
            if ((Y < 49) && (tileMap.CellIsPassable(X, Y + 1)))
            {
                adjacentNodes.Add(new PathNode<TMap, TCodes>(currentNode, endNode, new Vector2(X, Y + 1),
                                                             CostStraight + currentNode.DirectCost, tileMap));
            }
            else
            {
                downLeft = false;
                downRight = false;
            }

            if ((upLeft) && (tileMap.CellIsPassable(X - 1, Y - 1)))
            {
                adjacentNodes.Add(new PathNode<TMap, TCodes>(currentNode, endNode, new Vector2(X - 1, Y - 1),
                                                             CostDiagonal + currentNode.DirectCost, tileMap));
            }
            if ((upRight) && (tileMap.CellIsPassable(X + 1, Y - 1)))
            {
                adjacentNodes.Add(new PathNode<TMap, TCodes>(currentNode, endNode, new Vector2(X + 1, Y - 1),
                                                             CostDiagonal + currentNode.DirectCost, tileMap));
            }
            if ((downLeft) && (tileMap.CellIsPassable(X - 1, Y + 1)))
            {
                adjacentNodes.Add(new PathNode<TMap, TCodes>(currentNode, endNode, new Vector2(X - 1, Y + 1),
                                                             CostDiagonal + currentNode.DirectCost, tileMap));
            }
            if ((downRight) && (tileMap.CellIsPassable(X + 1, Y + 1)))
            {
                adjacentNodes.Add(new PathNode<TMap, TCodes>(currentNode, endNode, new Vector2(X + 1, Y + 1),
                                                             CostDiagonal + currentNode.DirectCost, tileMap));
            }
            return adjacentNodes;
        }

        #endregion

        #region Public Methods

        public static List<Vector2> FindReducedPath(Vector2 startTile, Vector2 endTile, TileMap<TMap, TCodes> tileMap)
        {
            List<Vector2> longPath = FindPath(startTile, endTile, tileMap);
            if (longPath == null)
                return null;

            int counter = 0;
            int removeCounter = 1;

            while ((counter + 2) < longPath.Count && counter + removeCounter < longPath.Count)
            {
                Vector2 startCell = tileMap.GetCellCenter(longPath[counter]);
                Vector2 endCell = tileMap.GetCellCenter(longPath[counter + 2]);
                Vector2 position = startCell;
                Vector2 direction = endCell - startCell;
                direction /= tileMap.TileHeight;
                bool noCollision = true;

                while (position != endCell)
                {
                    position += direction;
                    if (!tileMap.CellIsPassableByPixel(position))
                    {
                        noCollision = false;
                        break;
                    }
                }

                if (noCollision)
                {
                    longPath.RemoveAt(counter + removeCounter);
                }
                else
                {
                    ++counter;
                    removeCounter = 1;
                }
            }

            return longPath;
        }

        public static List<Vector2> FindPath(Vector2 startTile, Vector2 endTile, TileMap<TMap, TCodes> tileMap)
        {
            if (!tileMap.CellIsPassable(endTile) || !tileMap.CellIsPassable(startTile))
            {
                return null;
            }
            openList.Clear();
            nodeCosts.Clear();
            nodeStatus.Clear();

            var endNode = new PathNode<TMap, TCodes>(null, null, endTile, 0, tileMap);
            var startNode = new PathNode<TMap, TCodes>(null, endNode, startTile, 0, tileMap);

            AddNodeToOpenList(startNode);

            while (openList.Count > 0)
            {
                PathNode<TMap, TCodes> currentNode = openList[openList.Count - 1];
                if (currentNode.IsEqualToNode(endNode))
                {
                    var bestPath = new List<Vector2>();
                    while (currentNode != null)
                    {
                        bestPath.Insert(0, currentNode.GridLocation);
                        currentNode = currentNode.ParentNode;
                    }
                    return bestPath;
                }

                openList.Remove(currentNode);
                nodeCosts.Remove(currentNode.GridLocation);

                foreach (var possibleNode in FindAdjacentNodes(currentNode, endNode, tileMap))
                {
                    if (nodeStatus.ContainsKey(possibleNode.GridLocation))
                    {
                        if (nodeStatus[possibleNode.GridLocation] == NodeStatus.Closed)
                        {
                            continue;
                        }
                        if (nodeStatus[possibleNode.GridLocation] == NodeStatus.Open)
                        {
                            if (possibleNode.TotalCost >= nodeCosts[possibleNode.GridLocation])
                            {
                                continue;
                            }
                        }
                    }
                    AddNodeToOpenList(possibleNode);
                }
                nodeStatus[currentNode.GridLocation] = NodeStatus.Closed;
            }
            return null;
        }

        #endregion
    }
}