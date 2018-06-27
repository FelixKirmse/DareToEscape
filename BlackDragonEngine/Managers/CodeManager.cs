using System.Collections.Generic;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Providers;
using BlackDragonEngine.TileEngine;
using Microsoft.Xna.Framework;

namespace BlackDragonEngine.Managers
{
    public delegate void OnMapCodeCheckHandler<TCodes>(TCodes code, Vector2 location, GameObject player);

    public delegate void UnderPlayerCheckCodeHandler<TCodes>(TCodes code, GameObject player);

    public delegate int InPlayerCheckCodeHandler<TCodes>(
        TCodes code, List<TCodes> codes, Vector2 collisionCenter, int i, GameObject player);

    public static class CodeManager<TCodes>
    {
        public static event OnMapCodeCheckHandler<TCodes> OnMapCodeCheck;
        public static event UnderPlayerCheckCodeHandler<TCodes> OnCodeUnderPlayerCheck;
        public static event InPlayerCheckCodeHandler<TCodes> OnCodeInPlayerCenterCheck;

        public static void CheckCodes<TMap>() where TMap : IMap<TCodes>, new()
        {
            var tileMap = TileMap<TMap, TCodes>.GetInstance();
            EntityManager.ClearEntities();
            var player = VariableProvider.CurrentPlayer;
            EntityManager.SetPlayer();

            if (OnMapCodeCheck != null)
                foreach (var item in tileMap.Map.Codes)
                {
                    var location = new Vector2(item.Key.X * tileMap.TileWidth, item.Key.Y * tileMap.TileHeight);
                    foreach (var code in item.Value) OnMapCodeCheck(code, location, player);
                }
        }


        public static void CheckPlayerCodes<TMap>(TileMap<TMap, TCodes> tileMap) where TMap : IMap<TCodes>, new()
        {
            var player = VariableProvider.CurrentPlayer;
            if (OnCodeInPlayerCenterCheck != null)
                CheckCodesInPlayerCenter(player, tileMap);
            if (OnCodeUnderPlayerCheck != null)
                CheckCodesUnderPlayer(player, tileMap);
        }

        private static void CheckCodesUnderPlayer<TMap>(GameObject player, TileMap<TMap, TCodes> tileMap)
            where TMap : IMap<TCodes>, new()
        {
            var playerCollisionRectangle = player.CollisionRectangle;
            CheckCodesUnderPlayer(player, tileMap.GetCellCodes
            (
                tileMap.GetCellByPixel(
                    new Vector2(
                        playerCollisionRectangle.Left,
                        playerCollisionRectangle.Bottom
                    )
                )
            ));
            CheckCodesUnderPlayer(player, tileMap.GetCellCodes
            (
                tileMap.GetCellByPixel(
                    new Vector2(
                        playerCollisionRectangle.Right,
                        playerCollisionRectangle.Bottom
                    )
                )
            ));
            CheckCodesUnderPlayer(player, tileMap.GetCellCodes
            (
                tileMap.GetCellByPixel(
                    new Vector2(
                        playerCollisionRectangle.Center.X,
                        playerCollisionRectangle.Bottom
                    )
                )
            ));
        }

        private static void CheckCodesUnderPlayer(GameObject player, List<TCodes> codes)
        {
            if (codes == null)
                return;
            foreach (var code in codes) OnCodeUnderPlayerCheck(code, player);
        }

        private static void CheckCodesInPlayerCenter<TMap>(GameObject player, TileMap<TMap, TCodes> tileMap)
            where TMap : IMap<TCodes>, new()
        {
            var collisionCenter = player.RectCollisionCenter;
            collisionCenter /= tileMap.TileWidth;

            var codes = tileMap.GetCellCodes(collisionCenter);

            for (var i = 0; i < codes.Count; ++i)
                i = OnCodeInPlayerCenterCheck(codes[i], codes, collisionCenter, i, player);
        }
    }
}