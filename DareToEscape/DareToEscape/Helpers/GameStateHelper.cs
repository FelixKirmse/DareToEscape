using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DareToEscape.GameStates;
using BlackDragonEngine.Managers;
using BlackDragonEngine.GameStates;

namespace DareToEscape.Helpers
{
    static class GameStateHelper
    {
        private static Dictionary<string, GameStateManager> gameStateManagers;
        private static Dictionary<string, GameState> gameStates;

        public static void Initialize()
        {
            gameStateManagers = new Dictionary<string, GameStateManager>();
            gameStates = new Dictionary<string, GameState>();
        }

        public static GameStateManager InitializeGameStructure()
        {
            GameStateManager stateManager = new GameStateManager("GameStates");
            GameStateManager mainMenuManager = new GameStateManager("MainMenu");

            TitleScreenState titleState = new TitleScreenState("TitleScreen", true);

            stateManager.Push(titleState);
            AddGameState(titleState);
            AddGameStateManager(stateManager);
            return stateManager;
        }

        public static GameStateManager GetGameStateManager(string name)
        {
            return gameStateManagers[name];
        }

        public static void AddGameStateManager(GameStateManager manager)
        {
            gameStateManagers.Add(manager.Name, manager);
        }

        public static void RemoveGameStateManager(string name)
        {
            gameStateManagers.Remove(name);
        }

        public static GameState GetGameState(string name)
        {
            return gameStates[name];
        }

        public static void AddGameState(GameState state)
        {
            gameStates.Add(state.Name, state);
        }

        public static void RemoveGameState(string name)
        {
            gameStates.Remove(name);
        }

        public static void AddGameStateToManager(string stateName, string managerName)
        {
            GetGameStateManager(managerName).Push(GetGameState(stateName));
        }        
    }
}
