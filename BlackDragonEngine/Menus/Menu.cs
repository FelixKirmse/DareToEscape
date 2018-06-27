using System.Collections.Generic;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlackDragonEngine.Menus
{
    public class Menu
    {
        protected const string FontName = "Mono8";
        protected readonly List<MenuItem> MenuItems = new List<MenuItem>();
        protected readonly List<MenuLabel> MenuLabels = new List<MenuLabel>();
        protected readonly SpriteBatch SpriteBatch;
        public bool EnableMouseSelection = true;
        protected Vector2 ItemOffset = new Vector2(0, 16);

        public Menu()
        {
            SpriteBatch = VariableProvider.SpriteBatch;
        }

        protected string SelectedItem
        {
            get
            {
                foreach (var menuItem in MenuItems)
                    if (menuItem.IsSelected)
                        return menuItem.ItemName;
                return null;
            }
        }

        public virtual void Update()
        {
            if (InputMapper.StrictDown) NextMenuItem();

            if (InputMapper.StrictUp) PreviousMenuItem();

            if (EnableMouseSelection)
                ResolveMouseSelection();

            if (InputMapper.StrictAction || EnableMouseSelection || ShortCuts.LeftButtonClickedNowButNotLastFrame())
                SelectMenuItem();
            foreach (var menuItem in MenuItems) menuItem.Update();
        }

        public virtual void Draw()
        {
            SpriteBatch.Draw(
                VariableProvider.WhiteTexture,
                Vector2.Zero,
                new Rectangle(0, 0, VariableProvider.ClientBounds.Width,
                    VariableProvider.ClientBounds.Height),
                new Color(0, 0, 0, 200),
                0,
                Vector2.Zero,
                1,
                SpriteEffects.None,
                0.3f);

            foreach (var menuItem in MenuItems) menuItem.Draw();

            foreach (var menuLabel in MenuLabels) menuLabel.Draw();
        }

        protected void SetPositions()
        {
            for (var i = 0; i < MenuItems.Count; ++i)
                MenuItems[i].ItemPosition = ShortCuts.ScreenCenter -
                                            ShortCuts.GetFontCenter(FontName, MenuItems[i].ItemName) +
                                            (i - 2) * ItemOffset;
        }

        public virtual void NextMenuItem()
        {
            for (var i = 0; i < MenuItems.Count; ++i)
                if (MenuItems[i].IsSelected)
                {
                    MenuItems[i].IsSelected = false;
                    if (i == MenuItems.Count - 1)
                        MenuItems[0].IsSelected = true;
                    else
                        MenuItems[i + 1].IsSelected = true;
                    break;
                }
        }

        public virtual void PreviousMenuItem()
        {
            for (var i = 0; i < MenuItems.Count; ++i)
                if (MenuItems[i].IsSelected)
                {
                    MenuItems[i].IsSelected = false;
                    if (i == 0)
                        MenuItems[MenuItems.Count - 1].IsSelected = true;
                    else
                        MenuItems[i - 1].IsSelected = true;
                    break;
                }
        }

        public virtual void ResolveMouseSelection()
        {
            foreach (var menuItem in MenuItems)
                if (
                    ShortCuts.MouseIntersectsRectangle(ShortCuts.GetFontRectangle(menuItem.ItemPosition,
                        FontName,
                        menuItem.ItemName)))
                {
                    foreach (var item in MenuItems)
                        item.IsSelected = false;
                    menuItem.IsSelected = true;
                    break;
                }
        }

        protected void GetSelectedItem(out string selectedItem)
        {
            selectedItem = "";
            foreach (var menuItem in MenuItems)
                if (menuItem.IsSelected)
                {
                    selectedItem = menuItem.ItemName;
                    break;
                }
        }

        public virtual void SelectMenuItem()
        {
        }
    }
}