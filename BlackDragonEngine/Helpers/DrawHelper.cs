using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlackDragonEngine.Helpers
{
    public static class DrawHelper
    {
        private static readonly Queue<DrawOptions> alphaBlendStateBatch = new Queue<DrawOptions>();
        private static readonly Queue<DrawOptions> addBlendStateBatch = new Queue<DrawOptions>();

        public static void AddNewJob(DrawOptions o)
        {
            if (o.BlendState == BlendState.AlphaBlend)
                alphaBlendStateBatch.Enqueue(o);

            if (o.BlendState == BlendState.Additive)
                addBlendStateBatch.Enqueue(o);
        }

        public static void AddNewJob(BlendState blendState, Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float drawdepth)
        {
            AddNewJob(new DrawOptions(blendState, texture, position, sourceRectangle, color, rotation, origin, scale, effects, drawdepth));
        }

        public static void AddNewJob(Texture2D texture, Vector2 position, float drawDepth)
        {
            AddNewJob(new DrawOptions(texture, position, drawDepth));
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            int alphaJobCount = alphaBlendStateBatch.Count;
            int addJobCount = addBlendStateBatch.Count;

            if (alphaJobCount > 0)
            {
                spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
                for (int i = 0; i < alphaJobCount; ++i)
                {
                    DrawOptions o = alphaBlendStateBatch.Dequeue();
                    spriteBatch.Draw(o.Texture, o.Position, o.SourceRectangle, o.Color, o.Rotation, o.Origin, o.Scale, o.Effects, o.DrawDepth);
                }
                spriteBatch.End();
            }

            if (addJobCount > 0)
            {
                spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Additive);
                for (int i = 0; i < addJobCount; ++i)
                {
                    DrawOptions o = addBlendStateBatch.Dequeue();
                    spriteBatch.Draw(o.Texture, o.Position, o.SourceRectangle, o.Color, o.Rotation, o.Origin, o.Scale, o.Effects, o.DrawDepth);
                }
                spriteBatch.End();
            }
        }
    }

    public struct DrawOptions
    {
        public BlendState BlendState;

        public Texture2D Texture;
        public Vector2 Position;
        public Rectangle? SourceRectangle;
        public Color Color;
        public float Rotation;
        public Vector2 Origin;
        public float Scale;
        public SpriteEffects Effects;
        public float DrawDepth;

        public DrawOptions(BlendState blendState, Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float drawdepth)
        {
            BlendState = blendState;
            Texture = texture;
            Position = position;
            SourceRectangle = sourceRectangle;
            Color = color;
            Rotation = rotation;
            Origin = origin;
            Scale = scale;
            Effects = effects;
            DrawDepth = drawdepth;
        }

        public DrawOptions(Texture2D texture, Vector2 position, float drawDepth)
            : this(BlendState.AlphaBlend, texture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, drawDepth)
        { 
        }
    }
}
