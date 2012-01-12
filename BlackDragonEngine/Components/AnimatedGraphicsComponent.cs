using System.Collections.Generic;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlackDragonEngine.Components
{
    /// <summary>
    ///   Component used for handling animated sprites.
    /// </summary>
    public class AnimatedGraphicsComponent : GraphicsComponent
    {
        /// <summary>
        ///   Dictionary containing all animations
        /// </summary>
        protected Dictionary<string, AnimationStrip> Animations;

        /// <summary>
        ///   Represents the animation that is currently being played
        /// </summary>
        protected string CurrentAnimation;

        /// <summary>
        ///   Drawdepth of the sprite
        /// </summary>
        protected float DrawDepth = .91f;

        /// <summary>
        ///   Should the graphic be flipped?
        /// </summary>
        protected bool Flipped;

        /// <summary>
        ///   Represents the lates animation received by other components
        /// </summary>
        protected string ReceivedAnimation;

        /// <summary>
        ///   Draws the entity
        /// </summary>
        /// <param name = "obj">The entity to draw</param>
        public override void Draw(GameObject obj)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (Flipped)
                effects = SpriteEffects.FlipHorizontally;

            SpriteBatch.Draw(
                Animations[CurrentAnimation].Texture,
                obj.ScreenPosition,
                Animations[CurrentAnimation].FrameRectangle,
                Color.White,
                0,
                Vector2.Zero,
                1,
                effects,
                DrawDepth);
        }

        /// <summary>
        ///   Updates the animation
        /// </summary>
        /// <param name = "obj">The entity to update (not being used)</param>
        public override void Update(GameObject obj)
        {
            UpdateAnimation();
        }

        public override void Receive<T>(string message, T obj)
        {
            string[] messageParts = message.Split('_');

            if (messageParts[0] == "GRAPHICS")
            {
                if (messageParts[1] == "SET")
                {
                    if (messageParts[2] == "FLIPPED")
                    {
                        if (obj is bool)
                            Flipped = (bool) (object) obj;
                    }
                }

                if (messageParts[1] == "PLAYANIMATION")
                {
                    if (obj is string)
                        ReceivedAnimation = (string) (object) obj;
                }

                if (messageParts[1] == "SEND")
                {
                    if (messageParts[3] == "CURRENTANIMATION")
                    {
                        if (obj is GameObject)
                            ((GameObject) (object) obj).Send(messageParts[2] + "_SET_CURRENTANIMATION", CurrentAnimation);
                    }
                }
            }
        }

        /// <summary>
        ///   Updates the current Animation
        /// </summary>
        protected void UpdateAnimation()
        {
            if (Animations.ContainsKey(CurrentAnimation))
            {
                if (Animations[CurrentAnimation].FinishedPlaying)
                {
                    PlayAnimation(Animations[CurrentAnimation].NextAnimation);
                }
                else
                {
                    Animations[CurrentAnimation].Update();
                }
            }
        }

        /// <summary>
        ///   Plays a new animation
        /// </summary>
        /// <param name = "name">The name of the new animation</param>
        protected void PlayAnimation(string name)
        {
            if (name != null && Animations.ContainsKey(name))
            {
                CurrentAnimation = name;
                Animations[name].Play();
            }
        }
    }
}