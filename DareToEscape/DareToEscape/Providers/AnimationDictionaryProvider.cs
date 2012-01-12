using System.Collections.Generic;
using BlackDragonEngine.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape.Providers
{
    public static class AnimationDictionaryProvider
    {
        public static ContentManager Content { get; set; }


        public static Dictionary<string, AnimationStrip> GetPlayerAnimations()
        {
            var playerSheet = Content.Load<Texture2D>(@"textures/animations/player/playeranimations");
            var animations = new Dictionary<string, AnimationStrip>
                                 {
                                     {
                                         "Idle",
                                         new AnimationStrip(
                                         playerSheet, new Rectangle(0, 0, 16, 24), 1, "Idle")
                                         },
                                     {
                                         "Walk",
                                         new AnimationStrip(
                                         playerSheet, new Rectangle(0, 24, 160, 24), 10, "Walk")
                                         },
                                     {
                                         "JumpUp",
                                         new AnimationStrip(
                                         playerSheet, new Rectangle(0, 48, 16, 24), 1,
                                         "JumpUp")
                                         },
                                     {
                                         "JumpDown",
                                         new AnimationStrip(
                                         playerSheet, new Rectangle(16, 48, 32, 24), 2,
                                         "JumpDown", false)
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