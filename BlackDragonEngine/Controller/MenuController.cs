﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using BlackDragonEngine.Menus;
using BlackDragonEngine.Providers;
using BlackDragonEngine.Helpers;

namespace BlackDragonEngine.Controller
{
    public static class MenuController
    {
        public static void Update(Menu menu)
        {
            if (InputMapper.STRICTDOWN)
            {
                menu.NextMenuItem();
            }

            if (InputMapper.STRICTUP)
            {
                menu.PreviousMenuItem();
            }

            if(menu.EnableMouseSelection)
                menu.ResolveMouseSelection();            

            if (InputMapper.STRICTACTION || (menu.EnableMouseSelection || ShortcutProvider.LeftButtonClickedNowButNotLastFrame()))
            {                
                menu.SelectMenuItem();
            }
        }
    }
}
