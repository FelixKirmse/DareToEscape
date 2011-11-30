using System.Collections.Generic;
using BlackDragonEngine.Entities;
using DareToEscape.Editor;
using DareToEscape.GameStates;
using DareToEscape.Managers;

namespace DareToEscape.Providers
{
    internal static class GameVariableProvider
    {
        public static List<GameObject> Bosses = new List<GameObject>();
        public static BulletManager BulletManager { get; set; }
        public static EditorManager EditorManager { get; set; }
        public static IngameManager IngameManager { get; set; }
        public static MapGenerator MapGenerator { get; set; }
    }
}