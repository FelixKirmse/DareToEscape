using BlackDragonEngine.TileEngine;
using Microsoft.Xna.Framework;

namespace BlackDragonEngine.Helpers
{
    public static class Camera
    {
        #region Declarations

        private static Vector2 _position;
        private static Vector2 _viewPortSize;
        private static Rectangle _worldRectangle;

        #endregion

        #region Properties

        public static Vector2 ForcePosition
        {
            get { return _position; }
            set { _position = value; }
        }

        public static Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = new Vector2(
                    MathHelper.Clamp(value.X, _worldRectangle.X, _worldRectangle.Width - ViewPortWidth),
                    MathHelper.Clamp(value.Y, _worldRectangle.Y, _worldRectangle.Height - ViewPortHeight));
            }
        }

        public static Rectangle WorldRectangle
        {
            get { return _worldRectangle; }
            set { _worldRectangle = value; }
        }

        public static int ViewPortWidth
        {
            get { return (int) _viewPortSize.X; }
            set { _viewPortSize.X = value; }
        }

        public static int ViewPortHeight
        {
            get { return (int) _viewPortSize.Y; }
            set { _viewPortSize.Y = value; }
        }

        public static Rectangle ViewPort
        {
            get { return new Rectangle((int) Position.X, (int) Position.Y, ViewPortWidth, ViewPortHeight); }
        }

        #endregion

        #region Public Methods

        public static void Move(Vector2 offset)
        {
            Position += offset;
        }

        public static bool ObjectIsVisible(Rectangle bounds)
        {
            return ViewPort.Intersects(bounds);
        }

        public static Vector2 WorldToScreen(Vector2 worldLocation)
        {
            return worldLocation - _position;
        }

        public static Rectangle WorldToScreen(Rectangle worldRectangle)
        {
            return new Rectangle(worldRectangle.Left - (int) _position.X, worldRectangle.Top - (int) _position.Y,
                                 worldRectangle.Width, worldRectangle.Height);
        }

        public static Vector2 ScreenToWorld(Vector2 screenLocation)
        {
            return screenLocation + _position;
        }

        public static Rectangle ScreenToWorld(Rectangle screenRectangle)
        {
            return new Rectangle(screenRectangle.Left + (int) _position.X, screenRectangle.Top + (int) _position.Y,
                                 screenRectangle.Width, screenRectangle.Height);
        }

        public static void UpdateWorldRectangle()
        {
            WorldRectangle = new Rectangle(TileMap.Map.LowestX, TileMap.Map.LowestY, TileMap.TileWidth*(TileMap.MapWidth + 1),
                                           TileMap.MapHeight*(TileMap.TileHeight + 1));
        }

        #endregion
    }
}