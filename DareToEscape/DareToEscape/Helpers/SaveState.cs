using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace DareToEscape.Helpers
{
    [Serializable]
    public class SaveState
    {
        public List<string> Keys = new List<string>();
        public string CurrentLevel { get; set; }
        public Vector2 PlayerPosition { get; set; }
        public bool BossDead { get; set; }
    }
}