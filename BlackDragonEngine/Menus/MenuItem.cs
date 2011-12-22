using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlackDragonEngine.Menus
{
    public class MenuItem
    {
        private readonly SpriteFont _font;
        private Color _itemColor;

        public MenuItem(string itemName, Vector2 itemPosition, bool isSelected, SpriteFont font)
        {
            ItemName = itemName;
            ItemPosition = itemPosition;
            IsSelected = isSelected;
            _font = font;

            SelectedColor = Color.Red;
            UnSelectedcolor = Color.White;
        }

        public MenuItem(string itemName, Vector2 itemPosition)
            : this(itemName, itemPosition, false, FontProvider.GetFont("Pericles14"))
        {
        }

        public MenuItem(string itemName)
            : this(itemName, Vector2.Zero, false, FontProvider.GetFont("Pericles14"))
        {
        }

        public MenuItem(string itemName, string fontName)
            : this(itemName, Vector2.Zero, false, FontProvider.GetFont(fontName))
        {
        }

        public MenuItem(string itemName, string fontName, bool isSelected)
            : this(itemName, Vector2.Zero, isSelected, FontProvider.GetFont(fontName))
        {
        }

        public MenuItem(string itemName, string fontName, bool isSelected, Color selectedColor, Color unSelectedColor)
            : this(itemName, Vector2.Zero, isSelected, FontProvider.GetFont(fontName))
        {
            SelectedColor = selectedColor;
            UnSelectedcolor = unSelectedColor;
        }

        public string ItemName { get; set; }
        public Vector2 ItemPosition { get; set; }
        public bool IsSelected { get; set; }

        public Color SelectedColor { get; set; }
        public Color UnSelectedcolor { get; set; }


        public virtual void Update()
        {
            if (IsSelected)
                _itemColor = SelectedColor;
            else
                _itemColor = UnSelectedcolor;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(
                _font,
                ItemName,
                ShortCuts.Vector2Point(ItemPosition),
                _itemColor,
                0,
                Vector2.Zero,
                1,
                SpriteEffects.None,
                0.2f
                );
        }
    }
}