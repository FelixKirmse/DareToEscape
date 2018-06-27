using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using BlackDragonEngine.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape.Providers
{
    public static class BulletInformationProvider
    {
        private const string DataPath = @"\Content\data\bullets";
        private const string ImagePath = @"textures\spritesheets\bullets\";

        private const string Create = "Create";
        private const string Loop = "Loop";
        private const string Death = "Death";
        private const string Static = "Static";

        private static readonly Dictionary<int, Dictionary<string, AnimationStripStruct>> BulletAnimationStrips =
            new Dictionary<int, Dictionary<string, AnimationStripStruct>>(200);

        private static readonly Dictionary<int, BCircle> BulletBCircles = new Dictionary<int, BCircle>(200);


        public static Dictionary<string, AnimationStripStruct> GetAnimationStrip(int id)
        {
            return BulletAnimationStrips.ContainsKey(id) ? BulletAnimationStrips[id] : BulletAnimationStrips[0];
        }

        public static BCircle GetBCircle(int id)
        {
            return BulletBCircles.ContainsKey(id) ? BulletBCircles[id] : BulletBCircles[0];
        }

        public static void LoadBulletData(ContentManager content)
        {
            var startupPath = Application.StartupPath;
            foreach (var file in Directory.GetFiles(startupPath + DataPath))
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.Extension != ".txt") continue;
                var tmp = fileInfo.FullName.Split('\\');
                var sheetName = tmp[tmp.Length - 1].Split('.')[0];
                Texture2D texture;
                try
                {
                    texture = content.Load<Texture2D>(ImagePath + sheetName);
                }
                catch (Exception)
                {
                    continue;
                }

                using (var sr = new StreamReader(fileInfo.FullName))
                {
                    var currentLine = sr.ReadLine();
                    int cellWidth, cellHeight;
                    if (currentLine == null) continue;
                    tmp = currentLine.Split(':');
                    if (!int.TryParse(tmp[1].Split(',')[0], out cellWidth) ||
                        !int.TryParse(tmp[1].Split(',')[1], out cellHeight))
                        continue;

                    var cellsPerRow = texture.Width / cellWidth;

                    while ((currentLine = sr.ReadLine()) != null)
                        ImportLine(cellHeight, cellWidth, cellsPerRow, texture, currentLine);
                }
            }
        }

        private static void ImportLine(int cellHeight, int cellWidth, int cellsPerRow, Texture2D texture,
            string currentLine)
        {
            int cellNumber;
            int id;

            if (string.IsNullOrWhiteSpace(currentLine) || currentLine[0] == '/')
                return;

            var splitLine = currentLine.Split(';');
            if (!int.TryParse(splitLine[0], out id))
                return;
            if (!int.TryParse(splitLine[1], out cellNumber))
                return;
            float radius;
            var animations = GetAnimations(splitLine, cellNumber, cellsPerRow,
                cellWidth, cellHeight, texture,
                out radius);

            if (animations == null) return;
            BulletAnimationStrips.Add(id, animations);
            BulletBCircles.Add(id, new BCircle(cellWidth / 2f, cellHeight / 2f, radius));
        }

        private static Dictionary<string, AnimationStripStruct> GetAnimations(string[] splitLine, int cellNumber,
            int cellsPerRow,
            int cellWidth, int cellHeight,
            Texture2D texture,
            out float radius)
        {
            if (!float.TryParse(splitLine[5], NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out radius))
                return null;
            var animations = new Dictionary<string, AnimationStripStruct>();
            int createCount, loopCount, deathCount;
            if (!int.TryParse(splitLine[2].Split(':')[1], out createCount))
                return null;
            animations.Add(Create,
                GetAnimationStripStruct(cellNumber, cellsPerRow, cellWidth, cellHeight, texture, Create,
                    createCount, 0, false, .0625f, "Loop"));
            if (!int.TryParse(splitLine[3].Split(':')[1], out loopCount))
                return null;
            animations.Add(Loop,
                GetAnimationStripStruct(cellNumber, cellsPerRow, cellWidth, cellHeight, texture, Loop,
                    loopCount, createCount, true, .025f));
            if (!int.TryParse(splitLine[4].Split(':')[1], out deathCount))
                return null;
            animations.Add(Death,
                GetAnimationStripStruct(cellNumber, cellsPerRow, cellWidth, cellHeight, texture, Death,
                    deathCount, createCount + loopCount));
            return animations;
        }

        private static AnimationStripStruct GetAnimationStripStruct(int cellNumber, int cellsPerRow, int cellWidth,
            int cellHeight, Texture2D texture,
            string name = null, int frameCount = 1,
            int cellMod = 0, bool loop = false,
            float frameDelay = .05f, string nextAnimation = null)
        {
            var frameX = (cellNumber + cellMod) % cellsPerRow * cellWidth;
            var frameY = (cellNumber + cellMod) / cellsPerRow * cellHeight;
            return new AnimationStripStruct(texture,
                new Rectangle(frameX, frameY, cellWidth * frameCount, cellHeight),
                frameCount, name, loop) {NextAnimation = nextAnimation};
        }
    }
}