using System.Collections.Generic;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Providers;
using BlackDragonEngine.TileEngine;
using Microsoft.Xna.Framework;

namespace BlackDragonEngine.Managers
{
    public delegate void OnMapCodeCheckHandler(string[] code, Vector2 location, GameObject player);

    public delegate void UnderPlayerCheckCodeHandler(string[] code, GameObject player);

    public delegate int InPlayerCheckCodeHandler(
        string[] code, List<string> codes, Vector2 collisionCenter, int i, GameObject player);

    public static class CodeManager
    {
        public static event OnMapCodeCheckHandler OnMapCodeCheck;
        public static event UnderPlayerCheckCodeHandler OnCodeUnderPlayerCheck;
        public static event InPlayerCheckCodeHandler OnCodeInPlayerCenterCheck;

        public static void CheckCodes()
        {
            EntityManager.ClearEntities();
            GameObject player = VariableProvider.CurrentPlayer;
            EntityManager.SetPlayer();

            if (OnMapCodeCheck != null)
            {
                foreach (var item in TileMap.Map.Codes)
                {
                    var location = new Vector2(item.Key.X*TileMap.TileWidth, item.Key.Y*TileMap.TileHeight);
                    foreach (string codePart in item.Value)
                    {
                        string[] code = codePart.Split('_');
                        OnMapCodeCheck(code, location, player);
                    }
                }
            }
        }


        public static void CheckPlayerCodes()
        {
            GameObject player = VariableProvider.CurrentPlayer;
            if (OnCodeInPlayerCenterCheck != null)
                checkCodesInPlayerCenter(player);
            if (OnCodeUnderPlayerCheck != null)
                checkCodesUnderPlayer(player);
        }

        private static void checkCodesUnderPlayer(GameObject player)
        {
            Rectangle playerCollisionRectangle = player.CollisionRectangle;
            checkCodesUnderPlayer(player, TileMap.GetCellCodes
                                              (
                                                  TileMap.GetCellByPixel(
                                                      new Vector2(
                                                          playerCollisionRectangle.Left,
                                                          playerCollisionRectangle.Bottom
                                                          )
                                                      )
                                              ));
            checkCodesUnderPlayer(player, TileMap.GetCellCodes
                                              (
                                                  TileMap.GetCellByPixel(
                                                      new Vector2(
                                                          playerCollisionRectangle.Right,
                                                          playerCollisionRectangle.Bottom
                                                          )
                                                      )
                                              ));
            checkCodesUnderPlayer(player, TileMap.GetCellCodes
                                              (
                                                  TileMap.GetCellByPixel(
                                                      new Vector2(
                                                          playerCollisionRectangle.Center.X,
                                                          playerCollisionRectangle.Bottom
                                                          )
                                                      )
                                              ));
        }

        private static void checkCodesUnderPlayer(GameObject player, List<string> codes)
        {
            if (codes == null)
                return;
            foreach (string code in codes)
            {
                string[] codeArray = code.Split('_');
                OnCodeUnderPlayerCheck(codeArray, player);
            }
        }

        private static void checkCodesInPlayerCenter(GameObject player)
        {
            Vector2 collisionCenter = player.RectCollisionCenter;
            collisionCenter /= TileMap.TileWidth;

            List<string> codes = TileMap.GetCellCodes(collisionCenter);

            for (int i = 0; i < codes.Count; ++i)
            {
                string codePart = codes[i];
                string[] codeArray = codePart.Split('_');
                i = OnCodeInPlayerCenterCheck(codeArray, codes, collisionCenter, i, player);
            }
        }
    }
}