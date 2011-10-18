using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Helpers;

namespace BlackDragonEngine.Components
{
    /// <summary>
    /// Component used for handling animated sprites.
    /// </summary>
    public class AnimatedGraphicsComponent : GraphicsComponent
    {        
        /// <summary>
        /// Dictionary containing all animations
        /// </summary>
        protected Dictionary<string, AnimationStrip> animations;
        /// <summary>
        /// Should the graphic be flipped?
        /// </summary>
        protected bool flipped;
        /// <summary>
        /// Represents the animation that is currently being played
        /// </summary>
        protected string currentAnimation;
        /// <summary>
        /// Represents the lates animation received by other components
        /// </summary>
        protected string receivedAnimation;
        /// <summary>
        /// Drawdepth of the sprite
        /// </summary>
        new protected float drawDepth = .91f;
               
        /// <summary>
        /// Draws the entity
        /// </summary>
        /// <param name="obj">The entity to draw</param>
        /// <param name="spriteBatch">The spritebatch to use</param>
        public override void Draw(GameObject obj, SpriteBatch spriteBatch)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (flipped)
                effects = SpriteEffects.FlipHorizontally;

            spriteBatch.Draw(
                animations[currentAnimation].Texture,
                obj.ScreenPosition,
                animations[currentAnimation].FrameRectangle,
                Color.White,
                0,
                Vector2.Zero,
                1,
                effects,
                drawDepth);
        }

        /// <summary>
        /// Updates the animation
        /// </summary>
        /// <param name="obj">The entity to update (not being used)</param>
        public override void Update(GameObject obj)
        {
            updateAnimation();
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
                            flipped = (bool)(object)obj;
                    }                    
                }

                if (messageParts[1] == "PLAYANIMATION")
                {
                    if (obj is string)
                        receivedAnimation = (string)(object)obj;                    
                }

                if (messageParts[1] == "SEND")
                {
                    if (messageParts[3] == "CURRENTANIMATION")
                    {
                        if (obj is GameObject)
                            ((GameObject)(object)obj).Send(messageParts[2] + "_SET_CURRENTANIMATION", currentAnimation);
                    }
                }
            }        
        }

        /// <summary>
        /// Updates the current Animation
        /// </summary>
        protected void updateAnimation()
        {
            if (animations.ContainsKey(currentAnimation))
            {
                if (animations[currentAnimation].FinishedPlaying)
                {
                    PlayAnimation(animations[currentAnimation].NextAnimation);
                }
                else
                {
                    animations[currentAnimation].Update();
                }
            }
        }

        /// <summary>
        /// Plays a new animation
        /// </summary>
        /// <param name="name">The name of the new animation</param>
        protected void PlayAnimation(string name)
        {
            if (name != null && animations.ContainsKey(name))
            {
                currentAnimation = name;
                animations[name].Play();
            }
        }
    }
}
