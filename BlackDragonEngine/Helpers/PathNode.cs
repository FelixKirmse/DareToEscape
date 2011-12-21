using System;
using BlackDragonEngine.TileEngine;
using Microsoft.Xna.Framework;

namespace BlackDragonEngine.Helpers
{
    public sealed class PathNode<TMap, TCodes> where TMap : IMap<TCodes>, new()
    {
        #region Declarations

        public readonly float DirectCost;
        public readonly PathNode<TMap, TCodes> EndNode;
        public readonly PathNode<TMap, TCodes> ParentNode;
        public readonly float TotalCost;
        private readonly TileMap<TMap, TCodes> _tileMap;
        private Vector2 _gridLocation;

        #endregion

        #region Properties

        public Vector2 GridLocation
        {
            get { return _gridLocation; }
            set
            {
                _gridLocation = new Vector2(MathHelper.Clamp(value.X, 0f, _tileMap.MapWidth),
                                            MathHelper.Clamp(value.Y, 0f, _tileMap.MapHeight));
            }
        }

        public int GridX
        {
            get { return (int) _gridLocation.X; }
        }

        public int GridY
        {
            get { return (int) _gridLocation.Y; }
        }

        #endregion

        #region Constructor

        public PathNode(PathNode<TMap, TCodes> parentNode, PathNode<TMap, TCodes> endNode, Vector2 gridLocation,
                        float cost, TileMap<TMap, TCodes> tileMap)
        {
            ParentNode = parentNode;
            GridLocation = gridLocation;
            EndNode = endNode;
            DirectCost = cost;
            _tileMap = tileMap;
            if (endNode != null)
            {
                TotalCost = DirectCost + LinearCost();
            }
        }

        #endregion

        #region Helper Methods

        public float LinearCost()
        {
            return (1 + 1/1000)*(Math.Abs(GridX - EndNode.GridX) + Math.Abs(GridY - EndNode.GridY));
        }

        #endregion

        #region Public Methods

        public bool IsEqualToNode(PathNode<TMap, TCodes> node)
        {
            return GridLocation == node.GridLocation;
        }

        #endregion
    }
}