using BlackDragonEngine.TileEngine;
using Microsoft.Xna.Framework;

namespace BlackDragonEngine.Helpers
{
    public static class Camera
    {
        #region Declarations

        private static Vector2 _viewPortSize;

        #endregion

        #region Properties

        public static Vector2 ForcePosition { get; set; }

        public static Vector2 Position
        {
            get => ForcePosition;
            set => ForcePosition = new Vector2(
                MathHelper.Clamp(value.X, WorldRectangle.X, WorldRectangle.Width - ViewPortWidth),
                MathHelper.Clamp(value.Y, WorldRectangle.Y, WorldRectangle.Height - ViewPortHeight));
        }

        public static Rectangle WorldRectangle { get; set; }

        public static int ViewPortWidth
        {
            get => (int) _viewPortSize.X;
            set => _viewPortSize.X = value;
        }

        public static int ViewPortHeight
        {
            get => (int) _viewPortSize.Y;
            set => _viewPortSize.Y = value;
        }

        public static Rectangle ViewPort =>
            new Rectangle((int) Position.X, (int) Position.Y, ViewPortWidth, ViewPortHeight);

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
            return worldLocation - ForcePosition;
        }

        public static Rectangle WorldToScreen(Rectangle worldRectangle)
        {
            return new Rectangle(worldRectangle.Left - (int) ForcePosition.X,
                worldRectangle.Top - (int) ForcePosition.Y,
                worldRectangle.Width, worldRectangle.Height);
        }

        public static Vector2 ScreenToWorld(Vector2 screenLocation)
        {
            return screenLocation + ForcePosition;
        }

        public static Rectangle ScreenToWorld(Rectangle screenRectangle)
        {
            return new Rectangle(screenRectangle.Left + (int) ForcePosition.X,
                screenRectangle.Top + (int) ForcePosition.Y,
                screenRectangle.Width, screenRectangle.Height);
        }

        public static void UpdateWorldRectangle<TMap, TCodes>(TileMap<TMap, TCodes> tileMap)
            where TMap : IMap<TCodes>, new()
        {
            WorldRectangle = new Rectangle(tileMap.Map.LowestX * tileMap.TileWidth,
                tileMap.Map.LowestY * tileMap.TileHeight,
                tileMap.TileWidth * (tileMap.MapWidth + 1),
                tileMap.TileHeight * (tileMap.MapHeight + 1));
        }

        #endregion
    }
}