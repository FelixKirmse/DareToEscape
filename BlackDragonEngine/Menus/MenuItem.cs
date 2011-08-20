using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BlackDragonEngine.Providers;

namespace BlackDragonEngine.Menus
{
    public class MenuItem
    {
        public string ItemName { get; set; }
        public Vector2 ItemPosition { get; set; }
        public bool IsSelected { get; set; }

        private Color itemColor;
        private SpriteFont font;

        public MenuItem(string itemName, Vector2 itemPosition, bool isSelected, SpriteFont font) {
            ItemName = itemName;
            ItemPosition = itemPosition;
            IsSelected = isSelected;
            this.font = font;
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
        

        public void Update()
        {
            if (IsSelected)
                itemColor = Color.Red;
            else
                itemColor = Color.White;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(
                font,
                ItemName,
                ShortcutProvider.Vector2Point(ItemPosition),
                itemColor,
                0,
                Vector2.Zero,
                1,
                SpriteEffects.None,
                0.2f
                );
        }
    }
}
