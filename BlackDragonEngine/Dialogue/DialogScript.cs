using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace BlackDragonEngine.Dialogue
{
    public abstract class DialogScript
    {
        public string Text { get; protected set; }
        public Texture2D MugShot { get; protected set; }
        public string NextDialog { get; protected set; }
        public string SpeakerName { get; protected set; }
    }
}
