using DareToEscape.MapTools;
using Microsoft.Xna.Framework.Graphics;
using DareToEscape.Menus;
using BlackDragonEngine.Managers;
using DareToEscape.Helpers;
using BlackDragonEngine;
using DareToEscape.Editor;


namespace DareToEscape.Managers
{
    static class StateManager
    {
        public static GameStates GameState { get; set; }
        public static MenuStates MenuState { get; set; }        
        public static bool PlayerDead { get; set; }
        public static bool FastDead { get; set; }        

        public static void Initialize()
        {
            GameState = GameStates.Titlescreen;
            MenuState = MenuStates.Main;
            EngineStates.GameStates = EEngineStates.Running;
            PlayerDead = false;
            FastDead = true;            
            EngineStates.DialogState = DialogueStates.Inactive;
        }

        public static void Update()
        {
            if (GameState == GameStates.Editor)
            {
                EditorManager.Update();
            }
            else
            {
                if (EngineStates.DialogState == DialogueStates.Inactive)
                {
                    if (!PlayerDead)
                    {

                        if (EngineStates.GameStates == EEngineStates.Running)
                        {
                            switch (GameState)
                            {
                                case GameStates.Titlescreen:
                                    Titlescreen.Update();
                                    break;

                                case GameStates.Menu:
                                    MenuManager.Update();
                                    break;

                                case GameStates.Tutorial:
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
                else
                {
                    DialogManager.Update();
                }
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

                case GameStates.Tutorial:
                case GameStates.Ingame:
                    LevelManager.Draw(spriteBatch);
                    IngameManager.Draw(spriteBatch);
                    break;

                case GameStates.Editor:
                    EditorManager.Draw(spriteBatch);
                    LevelManager.Draw(spriteBatch);
                    break;

                case GameStates.GeneratingMap:
                    MapGenerator.Draw(spriteBatch);
                    break;
            }

            if (EngineStates.GameStates == EEngineStates.Paused)
            {
                LevelManager.Draw(spriteBatch);
                IngameManager.Draw(spriteBatch);
            }
                

            if (PlayerDead)
                GeneralHelper.Draw(spriteBatch);

            if (EngineStates.DialogState == DialogueStates.Active)
                DialogManager.Draw(spriteBatch);
        }
    }
}
