using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using DareToEscape.Menus;
using BlackDragonEngine.Managers;
using DareToEscape.Helpers;

namespace DareToEscape.Managers
{
    static class StateManager
    {
        public static GameStates GameState { get; set; }
        public static MenuStates MenuState { get; set; }
        public static bool GamePaused { get; set; }
        public static bool PlayerDead { get; set; }
        public static bool FastDead { get; set; }

        public static void Initialize()
        {
            GameState = GameStates.Titlescreen;
            MenuState = MenuStates.Main;
            GamePaused = false;
            PlayerDead = false;
        }

        public static void Update()
        {
            if (!PlayerDead)
            {
                if (!GamePaused)
                {
                    switch (GameState)
                    {
                        case GameStates.Titlescreen:
                            Titlescreen.Update();
                            break;

                        case GameStates.Menu:
                            MenuManager.Update();
                            break;

                        case GameStates.Ingame:
                            IngameManager.Update();
                            break;
                    }
                }
                else
                {
                    MenuManager.Update();
                }
            }
            else 
            {
                GeneralHelper.Update();
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            switch (GameState)
            {
                case GameStates.Titlescreen:
                    Titlescreen.Draw(spriteBatch);
                    break;

                case GameStates.Menu:
                    MenuManager.Draw(spriteBatch);
                    break;

                case GameStates.Ingame:
                    LevelManager.Draw();
                    break;
            }

            if (GamePaused)
                LevelManager.Draw();

            if (PlayerDead)
                GeneralHelper.Draw(spriteBatch);
        }
    }
}
