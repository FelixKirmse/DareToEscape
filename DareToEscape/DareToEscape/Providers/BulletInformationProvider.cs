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

        private static readonly List<Dictionary<string, AnimationStripStruct>> BulletAnimationStrips =
            new List<Dictionary<string, AnimationStripStruct>>();

        private static readonly List<BCircle> BulletBCircles = new List<BCircle>();


        public static Dictionary<string, AnimationStripStruct> GetAnimationStrip(int id)
        {
            try
            {
                return BulletAnimationStrips[id];
            }
            catch (Exception)
            {
                return BulletAnimationStrips[0];
            }
        }

        public static BCircle GetBCircle(int id)
        {
            try
            {
                return BulletBCircles[id];
            }
            catch (Exception)
            {
                return BulletBCircles[0];
            }
        }

        public static void LoadBulletData(ContentManager content)
        {
            string startupPath = Application.StartupPath;
            foreach (var file in Directory.GetFiles(startupPath + DataPath))
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.Extension != ".txt") continue;
                string[] tmp = fileInfo.FullName.Split('\\');
                string sheetName = tmp[tmp.Length - 1].Split('.')[0];
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
                    string currentLine = sr.ReadLine();
                    int cellWidth, cellHeight;
                    if (currentLine == null) continue;
                    tmp = currentLine.Split(':');
                    if (!(Int32.TryParse(tmp[1].Split(',')[0], out cellWidth)) ||
                        !(Int32.TryParse(tmp[1].Split(',')[1], out cellHeight)))
                        continue;

                    int cellsPerRow = texture.Width/cellWidth;

                    while ((currentLine = sr.ReadLine()) != null)
                    {
                        ImportLine(cellHeight, cellWidth, cellsPerRow, texture, currentLine);
                    }
                }
            }
        }

        private static void ImportLine(int cellHeight, int cellWidth, int cellsPerRow, Texture2D texture,
                                       string currentLine)
        {
            int cellNumber;

            if (String.IsNullOrWhiteSpace(currentLine) || currentLine[0] == '/')
                return;

            string[] splitLine = currentLine.Split(';');
            if (!int.TryParse(splitLine[0], out cellNumber))
                return;
            float radius = 0;
            Dictionary<string, AnimationStripStruct> animations = splitLine.Length == 2
                                                                      ? GetStaticAnimation(splitLine, cellNumber,
                                                                                           cellsPerRow, cellWidth,
                                                                                           cellHeight, texture,
                                                                                           out radius)
                                                                      : splitLine.Length == 5
                                                                            ? GetAnimations(splitLine, cellNumber,
                                                                                            cellsPerRow, cellWidth,
                                                                                            cellHeight, texture,
                                                                                            out radius)
                                                                            : null;
            if (animations == null) return;
            BulletAnimationStrips.Add(animations);
            BulletBCircles.Add(new BCircle(cellWidth/2f, cellHeight/2f, radius));
        }

        private static Dictionary<string, AnimationStripStruct> GetStaticAnimation(string[] splitLine, int cellNumber,
                                                                                   int cellsPerRow,
                                                                                   int cellWidth, int cellHeight,
                                                                                   Texture2D texture,
                                                                                   out float radius)
        {
            if (!float.TryParse(splitLine[1], NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out radius))
                return null;
            var animations = new Dictionary<string, AnimationStripStruct>
                                 {
                                     {
                                         Static,
                                         GetAnimationStripStruct(cellNumber, cellsPerRow, cellWidth, cellHeight, texture)
                                         }
                                 };
            return animations;
        }

        private static Dictionary<string, AnimationStripStruct> GetAnimations(string[] splitLine, int cellNumber,
                                                                              int cellsPerRow,
                                                                              int cellWidth, int cellHeight,
                                                                              Texture2D texture,
                                                                              out float radius)
        {
            if (!float.TryParse(splitLine[4], NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out radius))
                return null;
            var animations = new Dictionary<string, AnimationStripStruct>();
            int createCount, loopCount, deathCount;
            if (!int.TryParse(splitLine[1].Split(':')[1], out createCount))
                return null;
            animations.Add(Create,
                           GetAnimationStripStruct(cellNumber, cellsPerRow, cellWidth, cellHeight, texture, Create,
                                                   createCount, 0, false, "Loop"));
            if (!int.TryParse(splitLine[2].Split(':')[1], out loopCount))
                return null;
            animations.Add(Loop,
                           GetAnimationStripStruct(cellNumber, cellsPerRow, cellWidth, cellHeight, texture, Loop,
                                                   loopCount, createCount, true));
            if (!int.TryParse(splitLine[3].Split(':')[1], out deathCount))
                return null;
            animations.Add(Death,
                           GetAnimationStripStruct(cellNumber, cellsPerRow, cellWidth, cellHeight, texture, Death,
                                                   deathCount, createCount + loopCount));
            return animations;
        }

        private static AnimationStripStruct GetAnimationStripStruct(int cellNumber, int cellsPerRow, int cellWidth,
                                                                    int cellHeight, Texture2D texture,
                                                                    string name = null, int frameCount = 1,
                                                                    int cellMod = 0,bool loop = false, string nextAnimation = null)
        {
            int frameX = ((cellNumber + cellMod)%cellsPerRow)*cellWidth;
            int frameY = ((cellNumber + cellMod)/cellsPerRow)*cellHeight;
            return new AnimationStripStruct(texture,
                                            new Rectangle(frameX, frameY, cellWidth*frameCount, cellHeight),
                                            frameCount, name, loop) {NextAnimation = nextAnimation};
        }
    }
}