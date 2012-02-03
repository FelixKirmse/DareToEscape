using BlackDragonEngine.Managers;
using BlackDragonEngine.Providers;
using BlackDragonEngine.TileEngine;
using DareToEscape.Providers;
using Microsoft.Xna.Framework;

namespace DareToEscape.Helpers
{
    internal static class SaveHelper
    {
        public static void OnSave()
        {
            GameVariableProvider.SaveManager.CurrentSaveState.CurrentLevel = LevelManager.CurrentLevel;
            GameVariableProvider.SaveManager.CurrentSaveState.PlayerPosition = VariableProvider.CurrentPlayer.Position;
        }

        public static void OnLoad()
        {
            LevelManager.LoadLevel<Map<TileCode>, TileCode>(
                GameVariableProvider.SaveManager.CurrentSaveState.CurrentLevel);
            VariableProvider.CurrentPlayer.Position =
                new Vector2(GameVariableProvider.SaveManager.CurrentSaveState.PlayerPosition.X,
                            GameVariableProvider.SaveManager.CurrentSaveState.PlayerPosition.Y - 8);
            if (GameVariableProvider.SaveManager.CurrentSaveState.BossDead)
                foreach (var boss in GameVariableProvider.Bosses)
                    boss.Send("INACTIVE", "");
        }
    }
}