using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Providers;
using DareToEscape.Managers;
using DareToEscape.Providers;
using BlackDragonEngine.Entities;

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
            VariableProvider.CurrentPlayer.Position = new Microsoft.Xna.Framework.Vector2(SaveManager<SaveState>.CurrentSaveState.PlayerPosition.X, SaveManager<SaveState>.CurrentSaveState.PlayerPosition.Y - 8);
            if (SaveManager<SaveState>.CurrentSaveState.BossDead)            
                foreach(GameObject boss in GameVariableProvider.Bosses)
                    boss.Send("INACTIVE", "");
            
        }
    }
}
