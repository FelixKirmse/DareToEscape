using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlackDragonEngine.Menus
{
    public class MenuLabel
    {
        private readonly SpriteFont font;

        public MenuLabel(string text, string fontName)
        {
            Text = text;
            font = FontProvider.GetFont(fontName);
        }

        public string Text { get; set; }
        public Vector2 Position { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(
                font,
                Text,
                ShortCuts.Vector2Point(Position),
                Color.White,
                0,
                Vector2.Zero,
                1,
                SpriteEffects.None,
                .2f);
        }
    }
}