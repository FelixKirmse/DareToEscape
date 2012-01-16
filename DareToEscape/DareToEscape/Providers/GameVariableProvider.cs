using System.Collections.Generic;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Managers;
using DareToEscape.Helpers;

namespace DareToEscape.Providers
{
    public static class GameVariableProvider
    {
        public static List<GameObject> Bosses = new List<GameObject>();
        public static SaveManager<SaveState> SaveManager { get; set; }
    }
}