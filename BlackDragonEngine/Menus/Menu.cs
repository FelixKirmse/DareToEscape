using System.Collections.Generic;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlackDragonEngine.Menus
{
    public class Menu
    {
        public bool EnableMouseSelection = true;
        protected string fontName = "Mono8";
        protected Vector2 itemOffset = new Vector2(0, 16);
        protected List<MenuItem> menuItems = new List<MenuItem>();
        protected List<MenuLabel> menuLabels = new List<MenuLabel>();

        protected string SelectedItem
        {
            get
            {
                foreach (var menuItem in menuItems)
                {
                    if (menuItem.IsSelected)
                    {
                        return menuItem.ItemName;
                    }
                }
                return null;
            }
        }

        public virtual void Update()
        {
            if (InputMapper.StrictDown)
            {
                NextMenuItem();
            }

            if (InputMapper.StrictUp)
            {
                PreviousMenuItem();
            }

            if (EnableMouseSelection)
                ResolveMouseSelection();

            if (InputMapper.StrictAction ||
                (EnableMouseSelection || ShortCuts.LeftButtonClickedNowButNotLastFrame()))
            {
                SelectMenuItem();
            }
            foreach (var menuItem in menuItems)
            {
                menuItem.Update();
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                VariableProvider.WhiteTexture,
                Vector2.Zero,
                new Rectangle(0, 0, VariableProvider.Game.Window.ClientBounds.Width,
                              VariableProvider.Game.Window.ClientBounds.Height),
                new Color(0, 0, 0, 200),
                0,
                Vector2.Zero,
                1,
                SpriteEffects.None,
                0.3f);

            foreach (var menuItem in menuItems)
            {
                menuItem.Draw(spriteBatch);
            }

            foreach (var menuLabel in menuLabels)
            {
                menuLabel.Draw(spriteBatch);
            }
        }

        protected void SetPositions()
        {
            for (int i = 0; i < menuItems.Count; ++i)
            {
                menuItems[i].ItemPosition = ShortCuts.ScreenCenter -
                                            ShortCuts.GetFontCenter(fontName, menuItems[i].ItemName) +
                                            (i - 2)*itemOffset;
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
            foreach (var menuItem in menuItems)
            {
                if (
                    ShortCuts.MouseIntersectsRectangle(ShortCuts.GetFontRectangle(menuItem.ItemPosition,
                                                                                  fontName,
                                                                                  menuItem.ItemName)))
                {
                    foreach (var item in menuItems)
                        item.IsSelected = false;
                    menuItem.IsSelected = true;
                    break;
                }
            }
        }

        protected void GetSelectedItem(out string selectedItem)
        {
            selectedItem = "";
            foreach (var menuItem in menuItems)
            {
                if (menuItem.IsSelected)
                {
                    selectedItem = menuItem.ItemName;
                    break;
                }
            }
        }

        public virtual void SelectMenuItem()
        {
        }
    }
}