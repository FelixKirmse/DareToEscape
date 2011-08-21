using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Providers;
using DareToEscape.Managers;

namespace DareToEscape.Helpers
{
    static class SaveHelper
    {
        public static void OnSave()
        {
            SaveManager<SaveState>.CurrentSaveState.CurrentLevel = LevelManager.CurrentLevel;
            SaveManager<SaveState>.CurrentSaveState.PlayerPosition = VariableProvider.CurrentPlayer.Position;
            SaveManager<SaveState>.CurrentSaveState.FastDead = StateManager.FastDead;
        }

        public static void OnLoad()
        {
            LevelManager.LoadLevel(SaveManager<SaveState>.CurrentSaveState.CurrentLevel);
            VariableProvider.CurrentPlayer.Position = SaveManager<SaveState>.CurrentSaveState.PlayerPosition;
            StateManager.FastDead = SaveManager<SaveState>.CurrentSaveState.FastDead;
        }
    }
}
