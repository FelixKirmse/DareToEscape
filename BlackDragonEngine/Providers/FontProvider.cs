using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace BlackDragonEngine.Providers
{
    public static class FontProvider
    {
        private static readonly Dictionary<string, SpriteFont> Fonts = new Dictionary<string, SpriteFont>();

        public static void AddFont(string name, SpriteFont font)
        {
            Fonts.Add(name, font);
        }

        public static SpriteFont GetFont(string name)
        {
            return Fonts[name];
        }
    }
}