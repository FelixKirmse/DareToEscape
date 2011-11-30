using BlackDragonEngine;
using BlackDragonEngine.GameStates;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Providers;
using DareToEscape.GameStates;
using DareToEscape.Helpers;
using DareToEscape.Providers;

namespace DareToEscape.Managers
{
    internal sealed class GameStateManager : StateManager
    {
        public GameStateManager()
        {
            var editorManager = new EditorManager();
            var ingameManager = new IngameManager();
            var dialogManager = new DialogManager();
            var mapGenerator = new MapGenerator();
            var bulletManager = new BulletManager();

            DialogHelper.Initialize(dialogManager);

            VariableProvider.DialogManager = dialogManager;
            GameVariableProvider.BulletManager = bulletManager;
            GameVariableProvider.EditorManager = editorManager;
            GameVariableProvider.IngameManager = ingameManager;
            GameVariableProvider.MapGenerator = mapGenerator;

            AddGameState(bulletManager);
            AddGameState(editorManager);
            AddGameState(ingameManager);
            AddGameState(dialogManager);
            AddGameState(mapGenerator);
            AddGameState(new MenuManager());
            AddGameState(new GeneralHelper());
            AddGameState(new Titlescreen());

            FastDead = true;
            State = States.Titlescreen;
            MenuManager.MenuState = MenuStates.Main;
            EngineStates.GameStates = EEngineStates.Running;
            EngineStates.DialogState = DialogueStates.Inactive;
        }

        public static States State { get; set; }
        public static bool PlayerDead { get; set; }
        public static bool FastDead { get; private set; }
    }
}