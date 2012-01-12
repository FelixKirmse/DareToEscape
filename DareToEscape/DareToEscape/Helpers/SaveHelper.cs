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
            SaveManager<SaveState>.CurrentSaveState.CurrentLevel = LevelManager.CurrentLevel;
            SaveManager<SaveState>.CurrentSaveState.PlayerPosition = VariableProvider.CurrentPlayer.Position;
        }

        public static void OnLoad()
        {
            LevelManager.LoadLevel<Map<TileCode>, TileCode>(SaveManager<SaveState>.CurrentSaveState.CurrentLevel);
            VariableProvider.CurrentPlayer.Position =
                new Vector2(SaveManager<SaveState>.CurrentSaveState.PlayerPosition.X,
                            SaveManager<SaveState>.CurrentSaveState.PlayerPosition.Y - 8);
            if (SaveManager<SaveState>.CurrentSaveState.BossDead)
                foreach (var boss in GameVariableProvider.Bosses)
                    boss.Send("INACTIVE", "");
        }
    }
}