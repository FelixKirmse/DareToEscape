using BlackDragonEngine.Entities;
using BlackDragonEngine.Components;
using DareToEscape.Providers;
using Microsoft.Xna.Framework.Graphics;
using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework;
using DareToEscape.Entities;
using BlackDragonEngine.Helpers;

namespace DareToEscape.Components.PlayerComponents
{
    class PlayerGraphicsComponent : AnimatedGraphicsComponent
    {        
        private bool onGround;
        private bool focused;        

        public PlayerGraphicsComponent()
        {
            this.currentAnimation = "Idle";
            this.animations = AnimationDictionaryProvider.GetPlayerAnimations();
            this.drawDepth = .85f;
            focused = false;
        }

        public override void Update(GameObject obj)
        {            
            if(receivedAnimation == "")
            {
                if (onGround)
                    receivedAnimation = "Idle";
            }
                
            if (receivedAnimation != currentAnimation && receivedAnimation != "")
            {
                PlayAnimation(receivedAnimation);
            }
                        
            receivedAnimation = "";

            base.Update(obj);
        }  

        public override void Receive<T>(string message, T obj)
        {
            string[] messageParts = message.Split('_');

            if (messageParts[0] == "GRAPHICS")
            {
                if (messageParts[1] == "SET")
                {   
                    if (messageParts[2] == "ONGROUND")
                    {
                        if (obj is bool)
                            onGround = (bool)(object)obj;
                    }

                    if (messageParts[2] == "FOCUSED")
                    {
                        if (obj is bool)
                            focused = (bool)(object)obj;
                    }                   
                } 
            }

            base.Receive<T>(message, obj);
        }
    }
}
