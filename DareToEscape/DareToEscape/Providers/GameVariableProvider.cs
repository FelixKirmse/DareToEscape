using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
