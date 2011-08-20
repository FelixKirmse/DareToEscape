using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BlackDragonEngine.Providers;
using BlackDragonEngine.Controller;

namespace BlackDragonEngine.Menus
{
    public class Menu
    {
        protected Vector2 itemOffset = new Vector2(0, 32);
        protected List<MenuItem> menuItems = new List<MenuItem>();
        protected List<MenuLabel> menuLabels = new List<MenuLabel>();
        protected string fontName = "Mono21";
        public bool EnableMouseSelection = true;

        public virtual void Update()
        {
            MenuController.Update(this);
            foreach (MenuItem menuItem in menuItems)
            {
                menuItem.Update();
            }            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                VariableProvider.WhiteTexture,
                Vector2.Zero,
                new Rectangle(0, 0, 800, 600),
                new Color(0, 0, 0, 200),
                0,
                Vector2.Zero,
                1,
                SpriteEffects.None,
                0.3f);

            foreach (MenuItem menuItem in menuItems)
            {
                menuItem.Draw(spriteBatch);
            }

            foreach (MenuLabel menuLabel in menuLabels)
            {
                menuLabel.Draw(spriteBatch);
            }
        }

        protected void SetPositions()
        { 
            for (int i = 0; i < menuItems.Count; ++i)
                {
                    menuItems[i].ItemPosition = ShortcutProvider.ScreenCenter - ShortcutProvider.GetFontCenter(fontName, menuItems[i].ItemName) + (i-2) * itemOffset;
                } 
        }

        public virtual void NextMenuItem()
        {
            for (int i = 0; i < menuItems.Count; ++i)
            {
                if (menuItems[i].IsSelected)
                {
                    menuItems[i].IsSelected = false;
                    if (i == menuItems.Count - 1)
                        menuItems[0].IsSelected = true;
                    else
                        menuItems[i + 1].IsSelected = true;
                    break;
                }
            }
        }

        public virtual void PreviousMenuItem()
        {
            for (int i = 0; i < menuItems.Count; ++i)
            {
                if (menuItems[i].IsSelected)
                {
                    menuItems[i].IsSelected = false;
                    if (i == 0)
                        menuItems[menuItems.Count - 1].IsSelected = true;
                    else
                        menuItems[i - 1].IsSelected = true;
                    break;
                }
            }
        }

        public virtual void ResolveMouseSelection()
        {
            foreach (MenuItem menuItem in menuItems)
            {
                if (ShortcutProvider.MouseIntersectsRectangle(ShortcutProvider.GetFontRectangle(menuItem.ItemPosition, fontName, menuItem.ItemName)))
                {
                    foreach (MenuItem item in menuItems)
                        item.IsSelected = false;
                    menuItem.IsSelected = true;
                    break;
                }
            }
        }

        protected void GetSelectedItem(out string selectedItem)
        {
            selectedItem = "";
            foreach (MenuItem menuItem in menuItems)
            {
                if (menuItem.IsSelected)
                {
                    selectedItem = menuItem.ItemName;
                    break;
                }
            }
        }

        protected string SelectedItem
        {
            get
            {
                foreach (MenuItem menuItem in menuItems)
                {
                    if (menuItem.IsSelected)
                    {
                        return menuItem.ItemName;
                    }
                }
                return null;
            }
        }

        public virtual void SelectMenuItem()
        { 
        }
    }
}
