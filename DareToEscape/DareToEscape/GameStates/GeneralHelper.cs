using BlackDragonEngine;
using BlackDragonEngine.GameStates;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Providers;
using DareToEscape.Helpers;
using DareToEscape.Managers;
using DareToEscape.Providers;

namespace DareToEscape.GameStates
{
    internal class GeneralHelper : IUpdateableGameState
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
            GameVariableProvider.BulletManager.ClearAllBullets();
            VariableProvider.ScriptEngine.StopAllScripts();
            _elapsedSeconds += ShortcutProvider.ElapsedSeconds;
            if (GameStateManager.FastDead || _elapsedSeconds >= TimeToAutoResume || InputMapper.StrictAction)
            {
                if (SaveManager<SaveState>.CurrentSaveState.CurrentLevel != LevelManager.CurrentLevel)
                    LevelManager.ReloadLevel();
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