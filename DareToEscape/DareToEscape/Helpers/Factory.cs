using System.Collections.Generic;
using BlackDragonEngine.Components;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Providers;
using DareToEscape.Components.Entities;
using DareToEscape.Components.PlayerComponents;
using DareToEscape.Entities;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape.Helpers
{
    internal static class Factory
    {
        public static GameObject CreatePlayer()
        {
            var components = new List<Component>();
            components.Add(new PlayerGraphicsComponent());
            components.Add(new PlayerInputComponent());
            components.Add(new PlayerPhysicsComponent());
            components.Add(new PlayerSoundComponent());
            components.Add(new PlayerGeneralComponent());
            return new Player(components);
        }

        public static GameObject CreateCheckPoint()
        {
            var components = new List<Component>();
            components.Add(new CheckPointGraphicsComponent());
            return new GameObject(components);
        }

        public static GameObject CreateExit()
        {
            var components = new List<Component>();
            components.Add(new ExitGraphicsComponent());
            return new GameObject(components);
        }

        public static GameObject CreateKey()
        {
            var components = new List<Component>();
            components.Add(new KeyGraphicsComponent());
            return new GameObject(components);
        }

        public static GameObject CreateLock()
        {
            var components = new List<Component>();
            components.Add(new LockGraphicsComponent());
            return new GameObject(components);
        }

        public static GameObject CreateSmallTurret()
        {
            var components = new List<Component>();
            components.Add(new SmallTurretComponent());
            return new GameObject(components);
        }

        public static GameObject CreateMediumTurret()
        {
            var components = new List<Component>();
            components.Add(new MediumTurretComponent());
            return new GameObject(components);
        }

        public static GameObject CreateBoss1()
        {
            var components = new List<Component>();
            components.Add(new Boss1Component());
            return new GameObject(components);
        }

        public static GameObject CreateBossKiller()
        {
            var components = new List<Component>();
            components.Add(new BossKillerComponent());
            return new GameObject(components);
        }

        public static GameObject CreateSign()
        {
            var components = new List<Component>();
            components.Add(
                new GraphicsComponent(VariableProvider.Game.Content.Load<Texture2D>(@"textures/entities/sign")));
            return new GameObject(components);
        }

        public static GameObject CreateTutorialBoss()
        {
            var components = new List<Component>();
            components.Add(new TutorialBossComponent());
            return new GameObject(components);
        }
    }
}