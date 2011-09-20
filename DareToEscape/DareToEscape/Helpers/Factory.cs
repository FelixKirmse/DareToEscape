using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.Components;
using DareToEscape.Components;
using DareToEscape.Components.Player;
using BlackDragonEngine.Entities;
using DareToEscape.Components.Entities;
using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape.Helpers
{
    static class Factory
    {
        public static GameObject CreatePlayer()
        {
            List<Component> components = new List<Component>();
            components.Add(new PlayerGraphicsComponent());
            components.Add(new PlayerInputComponent());
            components.Add(new PlayerPhysicsComponent());
            components.Add(new PlayerSoundComponent());
            components.Add(new PlayerGeneralComponent());
            return new GameObject(components);
        }

        public static GameObject CreateCheckPoint()
        {
            List<Component> components = new List<Component>();
            components.Add(new CheckPointGraphicsComponent());
            return new GameObject(components);
        }

        public static GameObject CreateExit()
        {
            List<Component> components = new List<Component>();
            components.Add(new ExitGraphicsComponent());
            return new GameObject(components);
        }

        public static GameObject CreateKey()
        {
            List<Component> components = new List<Component>();
            components.Add(new KeyGraphicsComponent());
            return new GameObject(components);
        }

        public static GameObject CreateLock()
        {
            List<Component> components = new List<Component>();
            components.Add(new LockGraphicsComponent());
            return new GameObject(components);
        }

        public static GameObject CreateSmallTurret()
        {
            List<Component> components = new List<Component>();
            components.Add(new SmallTurretComponent());
            return new GameObject(components);
        }

        public static GameObject CreateMediumTurret()
        {
            List<Component> components = new List<Component>();
            components.Add(new MediumTurretComponent());
            return new GameObject(components);
        }

        public static GameObject CreateBoss1()
        {
            List<Component> components = new List<Component>();
            components.Add(new Boss1Component());
            return new GameObject(components);
        }

        public static GameObject CreateBossKiller()
        {
            List<Component> components = new List<Component>();
            components.Add(new BossKillerComponent());
            return new GameObject(components);
        }

        public static GameObject CreateSign()
        {
            List<Component> components = new List<Component>();
            components.Add(new GraphicsComponent(VariableProvider.Game.Content.Load<Texture2D>(@"textures/entities/sign")));
            return new GameObject(components);
        }

        public static GameObject CreateTutorialBoss()
        {
            List<Component> components = new List<Component>();
            components.Add(new TutorialBossComponent());
            return new GameObject(components);
        }

    }
}
