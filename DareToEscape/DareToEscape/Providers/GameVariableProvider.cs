using System.Collections.Generic;
using BlackDragonEngine.Entities;
using DareToEscape.Managers;

namespace DareToEscape.Providers
{
    static class GameVariableProvider
    {
        public static List<GameObject> Bosses = new List<GameObject>();
        public static BulletManager BulletManager { get; set; }
    }
}
