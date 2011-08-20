using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            animations.Add("Idle", new AnimationStrip(content.Load<Texture2D>(@"textures/animations/player/idle"), 16, "Idle", true, 1f));
            animations.Add("Walk", new AnimationStrip(content.Load<Texture2D>(@"textures/animations/player/walk"), 16, "Walk", true, .1f));
            animations.Add("JumpUp", new AnimationStrip(content.Load<Texture2D>(@"textures/animations/player/jumpup"), 16, "JumpUp", true, 1f));
            animations.Add("JumpDown", new AnimationStrip(content.Load<Texture2D>(@"textures/animations/player/jumpdown"), 16, "JumpDown", true, 1f));
            return animations;
        }
    }
}
