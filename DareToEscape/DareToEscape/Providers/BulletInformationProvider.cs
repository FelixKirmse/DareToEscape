﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.IO;
using System.Windows.Forms;
using BlackDragonEngine.Helpers;

namespace DareToEscape.Providers
{
    public static class BulletInformationProvider
    {
        private static readonly Dictionary<int, Rectangle> bulletGraphicRectangles = new Dictionary<int, Rectangle>();
        public static Texture2D BulletSheet { get; private set; }

        public static Rectangle GetSourceRectangle(int id)
        {
            return bulletGraphicRectangles[id];
        }

        public static BCircle GetBCircle(int id)
        {           
            Rectangle rect =  bulletGraphicRectangles[id];
            Vector2 center = rect.Center.ToVector2() - new Vector2(rect.X, rect.Y);
            float radius = rect.Width < rect.Height ? rect.Width * .33f : rect.Height * .33f;
            return new BCircle(center, radius);
        }

        public static void LoadBulletData(ContentManager content)
        {
            BulletSheet = content.Load<Texture2D>(@"textures/entities/bullets");
            using (StreamReader sr = new StreamReader(Application.StartupPath + @"/Content/data/shotdata.txt"))
            {
                string currentLine;
                int counter = 0;
                while ((currentLine = sr.ReadLine()) != null)
                {
                    ++counter;
                    int id;
                    int x, y, width, height;

                    if (String.IsNullOrWhiteSpace(currentLine))                    
                        continue;
                    if (currentLine[0] == '/')
                        continue;
                    string[] splitLine = currentLine.Split(';');
                    
                    if (!Int32.TryParse(splitLine[0], out id))
                        continue;

                    splitLine = splitLine[1].Split(',');
                    if (splitLine.Count() != 4)
                        continue;
                    if (!Int32.TryParse(splitLine[0], out x))
                        continue;
                    if (!Int32.TryParse(splitLine[1], out y))
                        continue;
                    if (!Int32.TryParse(splitLine[2], out width))
                        continue;
                    if (!Int32.TryParse(splitLine[3], out height))
                        continue;
                                        
                    bulletGraphicRectangles.Add(id, new Rectangle(x, y, width - x, height - y));
                }
            }
        }
    }
}
