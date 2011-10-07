﻿using BlackDragonEngine.Providers;
using DareToEscape.Managers;
using BlackDragonEngine.Managers;
using Microsoft.Xna.Framework.Graphics;
using BlackDragonEngine.Helpers;
using DareToEscape.Providers;

namespace DareToEscape.Helpers
{
    static class GeneralHelper
    {
        private static float timeToAutoResume = 3f;
        private static float elapsedSeconds;

        public static void Update()
        {
            GameVariableProvider.BulletManager.ClearAllBullets();
            VariableProvider.ScriptEngine.StopAllScripts();
            elapsedSeconds += ShortcutProvider.ElapsedSeconds;
            if (StateManager.FastDead ||elapsedSeconds >= timeToAutoResume || InputMapper.StrictAction)
            {
                if (SaveManager<SaveState>.CurrentSaveState.CurrentLevel != LevelManager.CurrentLevel)
                    LevelManager.ReloadLevel();
                else 
                    SaveManager<SaveState>.Load(VariableProvider.SaveSlot);
                StateManager.PlayerDead = false;
                elapsedSeconds = 0f;
            }                
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            LevelManager.Draw(spriteBatch);
            IngameManager.Draw(spriteBatch);
        }
    }
}
