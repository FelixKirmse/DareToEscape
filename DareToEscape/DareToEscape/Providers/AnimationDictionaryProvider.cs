using System.Collections.Generic;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape.Providers
{
    public static class AnimationDictionaryProvider
    {
        public static ContentManager Content { get; set; }


        public static Dictionary<string, AnimationStrip> GetPlayerAnimations()
        {
            var animations = new Dictionary<string, AnimationStrip>
                                 {
                                     {
                                         "Idle",
                                         new AnimationStrip(
                                         Content.Load<Texture2D>(@"textures/animations/player/idle"), 16, "Idle",
                                         true)
                                         },
                                     {
                                         "Walk",
                                         new AnimationStrip(
                                         Content.Load<Texture2D>(@"textures/animations/player/walk"), 16, "Walk",
                                         true, .05f)
                                         },
                                     {
                                         "JumpUp",
                                         new AnimationStrip(
                                         Content.Load<Texture2D>(@"textures/animations/player/jumpup"), 16,
                                         "JumpUp", true)
                                         },
                                     {
                                         "JumpDown",
                                         new AnimationStrip(
                                         Content.Load<Texture2D>(@"textures/animations/player/jumpdown"), 16,
                                         "JumpDown", false, .3f)
                                         }
                                 };
            return animations;
        }

        public static Dictionary<string, AnimationStrip> GetCheckPointAnimations()
        {
            var animations = new Dictionary<string, AnimationStrip>
                                 {
                                     {
                                         "Activated", new AnimationStrip(
                                         Content.Load<Texture2D>(@"textures/animations/entities/checkpoint/activated"),
                                         16,
                                         "Activated", true)
                                         },
                                     {
                                         "Deactivated", new AnimationStrip(
                                         Content.Load<Texture2D>(@"textures/animations/entities/checkpoint/deactivated"),
                                         16,
                                         "Deactivated", true)
                                         }
                                 };
            return animations;
        }

        public static Dictionary<string, AnimationStrip> GetExitAnimations()
        {
            var animations = new Dictionary<string, AnimationStrip>
                                 {
                                     {
                                         "Idle",
                                         new AnimationStrip(
                                         Content.Load<Texture2D>(@"textures/animations/entities/exit/idle"), 48,
                                         "Idle", true, .125f)
                                         }
                                 };
            return animations;
        }
    }
}