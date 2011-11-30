using BlackDragonEngine;
using BlackDragonEngine.GameStates;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Providers;
using DareToEscape.Editor;
using DareToEscape.GameStates;
using DareToEscape.Helpers;
using DareToEscape.Menus;
using DareToEscape.Providers;

namespace DareToEscape.Managers
{
    sealed class GameStateManager : StateManager 
    {
        public static States State { get; set; }
        public static bool PlayerDead { get; set; }
        public static bool FastDead { get; private set; }

        public GameStateManager()
        {
            var editorManager = new EditorManager();
            var ingameManager = new IngameManager();
            var dialogManager = new DialogManager();
            var mapGenerator = new MapGenerator();

            DialogHelper.Initialize(dialogManager);

            VariableProvider.DialogManager = dialogManager;
            GameVariableProvider.EditorManager = editorManager;
            GameVariableProvider.IngameManager = ingameManager;
            GameVariableProvider.MapGenerator = mapGenerator;

            AddGameState(editorManager);
            AddGameState(ingameManager);
            AddGameState(dialogManager);
            AddGameState(mapGenerator);
            AddGameState(new Titlescreen());
            AddGameState(new MenuManager());
            AddGameState(new GeneralHelper());

            FastDead = true;
            State = States.Titlescreen;
            MenuManager.MenuState = MenuStates.Main;
            EngineStates.GameStates = EEngineStates.Running;
            EngineStates.DialogState = DialogueStates.Inactive;
        }
    }
}
