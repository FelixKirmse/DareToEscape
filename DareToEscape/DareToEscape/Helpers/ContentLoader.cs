using BlackDragonEngine.Managers;
using BlackDragonEngine.Providers;
using BlackDragonEngine.TileEngine;
using DareToEscape.Menus;
using DareToEscape.Providers;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape.Helpers
{
    internal static class ContentLoader
    {
        public static void LoadContent(ContentManager Content)
        {
            FontProvider.AddFont("Mono14", Content.Load<SpriteFont>(@"fonts/mono14"));
            FontProvider.AddFont("Mono8", Content.Load<SpriteFont>(@"fonts/mono8"));
            FontProvider.AddFont("Mono21", Content.Load<SpriteFont>(@"fonts/mono21"));

            VariableProvider.WhiteTexture = Content.Load<Texture2D>(@"textures/white");

            Titlescreen.TitleTexture = Content.Load<Texture2D>(@"textures/titlescreen");

            DialogManager.Initialize();

            TileMap.Initialize(Content.Load<Texture2D>(@"textures/tilesheets/tilesheet"));

            BulletInformationProvider.LoadBulletData(Content);
        }
    }
}