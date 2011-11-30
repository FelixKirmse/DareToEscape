using System;
using BlackDragonEngine.TileEngine;
using Microsoft.Xna.Framework;

namespace BlackDragonEngine.Helpers
{
    public sealed class PathNode
    {
        #region Declarations

        public float DirectCost;
        public PathNode EndNode;
        public PathNode ParentNode;
        public float TotalCost;
        private Vector2 gridLocation;

        #endregion

        #region Properties

        public Vector2 GridLocation
        {
            get { return gridLocation; }
            set
            {
                gridLocation = new Vector2(MathHelper.Clamp(value.X, 0f, TileMap.MapWidth),
                                           MathHelper.Clamp(value.Y, 0f, TileMap.MapHeight));
            }
        }

        public int GridX
        {
            get { return (int) gridLocation.X; }
        }

        public int GridY
        {
            get { return (int) gridLocation.Y; }
        }

        #endregion

        #region Constructor

        public PathNode(PathNode parentNode, PathNode endNode, Vector2 gridLocation, float cost)
        {
            ParentNode = parentNode;
            GridLocation = gridLocation;
            EndNode = endNode;
            DirectCost = cost;
            if (!(endNode == null))
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

        public bool IsEqualToNode(PathNode node)
        {
            return GridLocation == node.GridLocation;
        }

        #endregion
    }
}