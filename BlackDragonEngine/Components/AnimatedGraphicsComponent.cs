using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Helpers;

namespace BlackDragonEngine.Components
{
    public class AnimatedGraphicsComponent : GraphicsComponent
    {
        protected Dictionary<string, AnimationStrip> animations;
        protected bool flipped;
        protected string currentAnimation;
        protected string receivedAnimation;
        new protected float drawDepth = .91f;
               
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
