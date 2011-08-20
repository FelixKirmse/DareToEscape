using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DareToEscape.Helpers
{
    [Serializable]
    class SaveState
    {
        public string CurrentLevel { get; set; }
        public Vector2 PlayerPosition { get; set; }
    }
}
