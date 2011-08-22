using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework.Graphics;
using DareToEscape.Managers;
using DareToEscape.Menus;
using BlackDragonEngine.Managers;

namespace DareToEscape.Helpers
{
    static class ContentLoader
    {
        public static void LoadContent(ContentManager Content)
        {
            FontProvider.AddFont("Mono14", Content.Load<SpriteFont>(@"fonts/mono14"));
            FontProvider.AddFont("Mono21", Content.Load<SpriteFont>(@"fonts/mono21"));                       

            VariableProvider.WhiteTexture = Content.Load<Texture2D>(@"textures/white");

            Titlescreen.TitleTexture = Content.Load<Texture2D>(@"textures/titlescreen");
        }
    }
}
