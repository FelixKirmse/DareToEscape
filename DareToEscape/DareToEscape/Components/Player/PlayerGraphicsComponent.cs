using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DareToEscape.Providers;

namespace DareToEscape.Components.Player
{
    class PlayerGraphicsComponent : AnimatedGraphicsComponent
    {        
        private bool onGround;

        public PlayerGraphicsComponent()
        {
            this.currentAnimation = "Idle";
            this.animations = AnimationDictionaryProvider.GetPlayerAnimations();
            this.drawDepth = .85f;
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
                } 
            }

            base.Receive<T>(message, obj);
        }
    }
}
