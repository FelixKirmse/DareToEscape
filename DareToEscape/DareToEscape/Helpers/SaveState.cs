using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using BlackDragonEngine.Entities;
using BlackDragonEngine.HelpMaps;

namespace DareToEscape.Helpers
{
    [Serializable]
    public class SaveState
    {
        public string CurrentLevel { get; set; }
        public Vector2 PlayerPosition { get; set; }
        public List<string> Keys = new List<string>();        
        public HelpMap CodeMap { get; set; }        
        public bool BossDead { get; set; }
    }
}
