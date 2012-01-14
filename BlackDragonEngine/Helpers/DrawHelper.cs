using System.Collections.Generic;
using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlackDragonEngine.Helpers
{
    public static class DrawHelper
    {
        private static readonly Queue<DrawOptions> AlphaBlendStateBatch = new Queue<DrawOptions>(10000);
        private static readonly Queue<DrawOptions> AddBlendStateBatch = new Queue<DrawOptions>(10000);

        public static void AddNewJob(DrawOptions o)
        {
            if (o.BlendState == BlendState.AlphaBlend)
                AlphaBlendStateBatch.Enqueue(o);

            if (o.BlendState == BlendState.Additive)
                AddBlendStateBatch.Enqueue(o);
        }

        public static void AddNewJob(BlendState blendState, Texture2D texture, Vector2 position,
                                     Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin,
                                     float scale, SpriteEffects effects, float drawdepth)
        {
            AddNewJob(new DrawOptions(blendState, texture, position, sourceRectangle, color, rotation, origin, scale,
                                      effects, drawdepth));
        }

        public static void AddNewJob(Texture2D texture, Vector2 position, float drawDepth)
        {
            AddNewJob(new DrawOptions(texture, position, drawDepth));
        }

        public static void Draw()
        {
            SpriteBatch spriteBatch = VariableProvider.SpriteBatch;
            int alphaJobCount = AlphaBlendStateBatch.Count;
            int addJobCount = AddBlendStateBatch.Count;

            if (alphaJobCount > 0)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);
                for (int i = 0; i < alphaJobCount; ++i)
                {
                    DrawOptions o = AlphaBlendStateBatch.Dequeue();
                    spriteBatch.Draw(o.Texture, o.Position, o.SourceRectangle, o.Color, o.Rotation, o.Origin, o.Scale,
                                     o.Effects, o.DrawDepth);
                }
                spriteBatch.End();
            }

            if (addJobCount > 0)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointClamp, null, null);
                for (int i = 0; i < addJobCount; ++i)
                {
                    DrawOptions o = AddBlendStateBatch.Dequeue();
                    spriteBatch.Draw(o.Texture, o.Position, o.SourceRectangle, o.Color, o.Rotation, o.Origin, o.Scale,
                                     o.Effects, o.DrawDepth);
                }
                spriteBatch.End();
            }
        }
    }

    public struct DrawOptions
    {
        public BlendState BlendState;

        public Color Color;
        public float DrawDepth;
        public SpriteEffects Effects;
        public Vector2 Origin;
        public Vector2 Position;
        public float Rotation;
        public float Scale;
        public Rectangle? SourceRectangle;
        public Texture2D Texture;

        public DrawOptions(BlendState blendState, Texture2D texture, Vector2 position, Rectangle? sourceRectangle,
                           Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects,
                           float drawdepth)
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
            : this(
                BlendState.AlphaBlend, texture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None,
                drawDepth)
        {
        }
    }
}