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
        private static readonly Dictionary<Vector2, NodeStatus> NodeStati = new Dictionary<Vector2, NodeStatus>();

        private static readonly List<PathNode<TMap, TCodes>> OpenList = new List<PathNode<TMap, TCodes>>();

        private static readonly Dictionary<Vector2, float> NodeCosts = new Dictionary<Vector2, float>();

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

            while ((OpenList.Count() > index) && (cost < OpenList[index].TotalCost))
            {
                ++index;
            }
            OpenList.Insert(index, node);
            NodeCosts[node.GridLocation] = node.TotalCost;
            NodeStati[node.GridLocation] = NodeStatus.Open;
        }

        private static List<PathNode<TMap, TCodes>> FindAdjacentNodes(PathNode<TMap, TCodes> currentNode,
                                                                      PathNode<TMap, TCodes> endNode,
                                                                      TileMap<TMap, TCodes> tileMap, bool ignoreWalls)
        {
            var adjacentNodes = new List<PathNode<TMap, TCodes>>();

            int X = currentNode.GridX;
            int Y = currentNode.GridY;

            bool upLeft = true;
            bool upRight = true;
            bool downLeft = true;
            bool downRight = true;

            if (ignoreWalls || tileMap.CellIsPassable(X - 1, Y))
            {
                adjacentNodes.Add(new PathNode<TMap, TCodes>(currentNode, endNode, new Vector2(X - 1, Y),
                                                             CostStraight + currentNode.DirectCost, tileMap));
            }
            else
            {
                upLeft = false;
                downLeft = false;
            }

            if (ignoreWalls || tileMap.CellIsPassable(X + 1, Y))
            {
                adjacentNodes.Add(new PathNode<TMap, TCodes>(currentNode, endNode, new Vector2(X + 1, Y),
                                                             CostStraight + currentNode.DirectCost, tileMap));
            }
            else
            {
                upRight = false;
                downRight = false;
            }

            if (ignoreWalls || tileMap.CellIsPassable(X, Y - 1))
            {
                adjacentNodes.Add(new PathNode<TMap, TCodes>(currentNode, endNode, new Vector2(X, Y - 1),
                                                             CostStraight + currentNode.DirectCost, tileMap));
            }
            else
            {
                upLeft = false;
                upRight = false;
            }
            if (ignoreWalls || tileMap.CellIsPassable(X, Y + 1))
            {
                adjacentNodes.Add(new PathNode<TMap, TCodes>(currentNode, endNode, new Vector2(X, Y + 1),
                                                             CostStraight + currentNode.DirectCost, tileMap));
            }
            else
            {
                downLeft = false;
                downRight = false;
            }

            if (upLeft && (ignoreWalls || tileMap.CellIsPassable(X - 1, Y - 1)))
            {
                adjacentNodes.Add(new PathNode<TMap, TCodes>(currentNode, endNode, new Vector2(X - 1, Y - 1),
                                                             CostDiagonal + currentNode.DirectCost, tileMap));
            }
            if (upRight && (ignoreWalls || tileMap.CellIsPassable(X + 1, Y - 1)))
            {
                adjacentNodes.Add(new PathNode<TMap, TCodes>(currentNode, endNode, new Vector2(X + 1, Y - 1),
                                                             CostDiagonal + currentNode.DirectCost, tileMap));
            }
            if (downLeft && (ignoreWalls || tileMap.CellIsPassable(X - 1, Y + 1)))
            {
                adjacentNodes.Add(new PathNode<TMap, TCodes>(currentNode, endNode, new Vector2(X - 1, Y + 1),
                                                             CostDiagonal + currentNode.DirectCost, tileMap));
            }
            if (downRight && (ignoreWalls || tileMap.CellIsPassable(X + 1, Y + 1)))
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

        public static List<Vector2> FindPath(Vector2 startTile, Vector2 endTile, TileMap<TMap, TCodes> tileMap,
                                             bool ignoreWalls = false)
        {
            if ((!tileMap.CellIsPassable(endTile) || !tileMap.CellIsPassable(startTile)) && !ignoreWalls)
            {
                return null;
            }
            OpenList.Clear();
            NodeCosts.Clear();
            NodeStati.Clear();

            var endNode = new PathNode<TMap, TCodes>(null, null, endTile, 0, tileMap);
            var startNode = new PathNode<TMap, TCodes>(null, endNode, startTile, 0, tileMap);

            AddNodeToOpenList(startNode);

            while (OpenList.Count > 0)
            {
                PathNode<TMap, TCodes> currentNode = OpenList[OpenList.Count - 1];
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

                OpenList.Remove(currentNode);
                NodeCosts.Remove(currentNode.GridLocation);

                foreach (var possibleNode in FindAdjacentNodes(currentNode, endNode, tileMap, ignoreWalls))
                {
                    if (NodeStati.ContainsKey(possibleNode.GridLocation))
                    {
                        if (NodeStati[possibleNode.GridLocation] == NodeStatus.Closed)
                        {
                            continue;
                        }
                        if (NodeStati[possibleNode.GridLocation] == NodeStatus.Open)
                        {
                            if (possibleNode.TotalCost >= NodeCosts[possibleNode.GridLocation])
                            {
                                continue;
                            }
                        }
                    }
                    AddNodeToOpenList(possibleNode);
                }
                NodeStati[currentNode.GridLocation] = NodeStatus.Closed;
            }
            return null;
        }

        #endregion
    }
}