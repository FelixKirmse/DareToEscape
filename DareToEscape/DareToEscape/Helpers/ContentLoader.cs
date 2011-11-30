using BlackDragonEngine.Managers;
using BlackDragonEngine.Providers;
using BlackDragonEngine.TileEngine;
using DareToEscape.GameStates;
using DareToEscape.Menus;
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

            VariableProvider.WhiteTexture = content.Load<Texture2D>(@"textures/white");

            Titlescreen.TitleTexture = content.Load<Texture2D>(@"textures/titlescreen");

            TileMap.Initialize(content.Load<Texture2D>(@"textures/tilesheets/tilesheet"));

            BulletInformationProvider.LoadBulletData(content);
        }
    }
}