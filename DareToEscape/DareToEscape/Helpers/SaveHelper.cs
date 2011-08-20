﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Providers;

namespace DareToEscape.Helpers
{
    static class SaveHelper
    {
        public static void OnSave()
        {
            SaveManager<SaveState>.CurrentSaveState.CurrentLevel = LevelManager.CurrentLevel;
            SaveManager<SaveState>.CurrentSaveState.PlayerPosition = VariableProvider.CurrentPlayer.Position;
        }

        public static void OnLoad()
        {
            LevelManager.LoadLevel(SaveManager<SaveState>.CurrentSaveState.CurrentLevel);
            VariableProvider.CurrentPlayer.Position = SaveManager<SaveState>.CurrentSaveState.PlayerPosition;
        }
    }
}
