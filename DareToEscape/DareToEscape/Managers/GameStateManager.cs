using BlackDragonEngine;
using BlackDragonEngine.GameStates;
using BlackDragonEngine.Managers;
using DareToEscape.GameStates;
using DareToEscape.Helpers;

namespace DareToEscape.Managers
{
    internal sealed class GameStateManager : StateManager
    {
        public GameStateManager()
        {
            var editorManager = EditorManager.GetInstance();
            var ingameManager = Ingame.GetInstance();
            var dialogManager = DialogManager.GetInstance();
            var mapGenerator = MapGenerator.GetInstance();
            var bulletManager = BulletManager.GetInstance();

            DialogHelper.Initialize(dialogManager);

            AddGameState(bulletManager);
            AddGameState(editorManager);
            AddGameState(ingameManager);
            AddGameState(dialogManager);
            AddGameState(mapGenerator);
            AddGameState(new Menu());
            AddGameState(new GeneralHelper());
            AddGameState(new Titlescreen());

            FastDead = true;
            State = States.Titlescreen;
            Menu.MenuState = MenuStates.Main;
            EngineStates.GameStates = EEngineStates.Running;
            EngineStates.DialogState = DialogueStates.Inactive;
        }

        public static States State { get; set; }
        public static bool PlayerDead { get; set; }
        public static bool FastDead { get; private set; }
    }
}