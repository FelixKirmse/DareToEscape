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
            Ingame ingameManager = Ingame.GetInstance();
            DialogManager dialogManager = DialogManager.GetInstance();
            BulletManager bulletManager = BulletManager.GetInstance();

            DialogHelper.Initialize(dialogManager);

            AddGameState(bulletManager);
            AddGameState(ingameManager);
            AddGameState(dialogManager);
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