﻿using BlackDragonEngine;
using BlackDragonEngine.GameStates;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Providers;
using BlackDragonEngine.TileEngine;
using DareToEscape.Helpers;
using DareToEscape.Managers;

namespace DareToEscape.GameStates
{
    internal sealed class GeneralHelper : IUpdateableGameState
    {
        private const float TimeToAutoResume = 3f;
        private float _elapsedSeconds;

        #region IUpdateableGameState Members

        public bool UpdateCondition
        {
            get
            {
                return GameStateManager.PlayerDead && EngineStates.DialogState == DialogueStates.Inactive &&
                       GameStateManager.State != States.Editor;
            }
        }

        public bool Update()
        {
            BulletManager.GetInstance().ClearAllBullets();
            VariableProvider.ScriptEngine.StopAllScripts();
            _elapsedSeconds += ShortCuts.ElapsedSeconds;
            if (GameStateManager.FastDead || _elapsedSeconds >= TimeToAutoResume || InputMapper.StrictAction)
            {
                if (SaveManager<SaveState>.CurrentSaveState.CurrentLevel != LevelManager.CurrentLevel)
                    LevelManager.ReloadLevel<Map<TileCode>, TileCode>();
                else
                    SaveManager<SaveState>.Load(VariableProvider.SaveSlot);
                GameStateManager.PlayerDead = false;
                _elapsedSeconds = 0f;
            }
            return true;
        }

        #endregion
    }
}