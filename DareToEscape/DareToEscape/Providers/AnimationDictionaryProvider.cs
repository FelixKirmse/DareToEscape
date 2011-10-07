using System.Collections.Generic;
using BlackDragonEngine.Helpers;
using Microsoft.Xna.Framework.Content;
using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape.Providers
{
    static class AnimationDictionaryProvider
    {
        private static ContentManager content = VariableProvider.Game.Content;


        public static Dictionary<string, AnimationStrip> GetPlayerAnimations()
        {
            Dictionary<string, AnimationStrip> animations = new Dictionary<string, AnimationStrip>();
            animations.Add("Idle", new AnimationStrip(content.Load<Texture2D>(@"textures/animations/player/idle"), 16, "Idle", true));
            animations.Add("Walk", new AnimationStrip(content.Load<Texture2D>(@"textures/animations/player/walk"), 16, "Walk", true, .1f));
            animations.Add("JumpUp", new AnimationStrip(content.Load<Texture2D>(@"textures/animations/player/jumpup"), 16, "JumpUp", true));
            animations.Add("JumpDown", new AnimationStrip(content.Load<Texture2D>(@"textures/animations/player/jumpdown"), 16, "JumpDown", true));
            return animations;
        }

        public static Dictionary<string, AnimationStrip> GetCheckPointAnimations()
        {
            Dictionary<string, AnimationStrip> animations = new Dictionary<string, AnimationStrip>();
            animations.Add("Activated", new AnimationStrip(content.Load<Texture2D>(@"textures/animations/entities/checkpoint/activated"), 16, "Activated", true));
            animations.Add("Deactivated", new AnimationStrip(content.Load<Texture2D>(@"textures/animations/entities/checkpoint/deactivated"), 16, "Deactivated", true));
            return animations;
        }

        public static Dictionary<string, AnimationStrip> GetExitAnimations()
        {
            Dictionary<string, AnimationStrip> animations = new Dictionary<string, AnimationStrip>();
            animations.Add("Idle", new AnimationStrip(content.Load<Texture2D>(@"textures/animations/entities/exit/idle"), 48, "Idle", true, .125f));            
            return animations;
        }
    }
}
