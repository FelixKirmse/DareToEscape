using System;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Scripting;
using BlackDragonEngine.TileEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlackDragonEngine.Providers
{
    public static class VariableProvider
    {
        public static Game Game { get; set; }
        public static Texture2D WhiteTexture { get; set; }
        public static GameObject CurrentPlayer { get; set; }
        public static Random RandomSeed { get; set; }
        public static GameTime GameTime { get; set; }
        public static string SaveSlot { get; set; }
        public static ScriptEngine ScriptEngine { get; set; }
        public static int ProcessorCount { get; set; }
        public static CoordList CoordList { get; set; }

        public static void GenerateNewRandomSeed()
        {
            RandomSeed = new Random();
        }
    }
}