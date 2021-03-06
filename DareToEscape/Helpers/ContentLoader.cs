﻿using BlackDragonEngine.Providers;
using BlackDragonEngine.TileEngine;
using DareToEscape.GameStates;
using DareToEscape.Providers;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape.Helpers
{
    internal static class ContentLoader
    {
        public static void LoadContent(ContentManager content)
        {
            FontProvider.AddFont("Mono14", content.Load<SpriteFont>(@"fonts/mono14"));
            FontProvider.AddFont("Mono8", content.Load<SpriteFont>(@"fonts/mono8"));
            FontProvider.AddFont("Mono21", content.Load<SpriteFont>(@"fonts/mono21"));

            Titlescreen.TitleTexture = content.Load<Texture2D>(@"textures/titlescreen");

            new TileMap<Map<TileCode>, TileCode>(8, 8, 0, FontProvider.GetFont("Mono8"),
                content.Load<Texture2D>(@"textures/spritesheets/tilesheet"), 3);

            BulletInformationProvider.LoadBulletData(content);
            AnimationDictionaryProvider.Content = content;
        }
    }
}