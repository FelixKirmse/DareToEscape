using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.Entities;
using Microsoft.Xna.Framework;
using BlackDragonEngine.Providers;
using BlackDragonEngine.TileEngine;

namespace BlackDragonEngine.Managers
{
    public delegate void OnMapCodeCheckHandler(string[] code, Vector2 location, GameObject player);
    public delegate void UnderPlayerCheckCodeHandler(string[] code, GameObject player);
    public delegate int InPlayerCheckCodeHandler(string[] code, MapSquare square, Vector2 collisionCenter,int i, GameObject player);

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
                for (int y = 0; y < TileMap.MapHeight; ++y)
                {
                    for (int x = 0; x < TileMap.MapWidth; ++x)
                    {
                        Vector2 location = new Vector2(x * TileMap.TileWidth, y * TileMap.TileHeight);
                        foreach (string codePart in TileMap.GetCellCodes(x, y))
                        {
                            string[] code = codePart.Split('_');
                            OnMapCodeCheck(code, location, player);
                        }
                    }
                }
            }
        }


        public static void CheckPlayerCodes()
        {
            GameObject player = VariableProvider.CurrentPlayer;
            if(OnCodeInPlayerCenterCheck != null)
                checkCodesInPlayerCenter(player);   
            if(OnCodeUnderPlayerCheck != null)
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

        private static void checkCodesUnderPlayer(GameObject player, List<string> codes )
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
            Vector2 collisionCenter = player.CollisionCenter;

            MapSquare square = TileMap.GetMapSquareAtPixel(collisionCenter);
            if ( square == null)
            {
                return;
            }

            for (int i = 0; i < square.Codes.Count; ++i )
            {
                string codePart = TileMap.GetMapSquareAtPixel(collisionCenter).Codes[i];
                string[] codeArray = codePart.Split('_');
                i = OnCodeInPlayerCenterCheck(codeArray,square, collisionCenter, i, player);
            }
        }        
    }
}
