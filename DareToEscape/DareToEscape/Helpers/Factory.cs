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
    public static class Factory
    {
        public static GameObject CreatePlayer()
        {
            var components = new List<IComponent>
                                 {
                                     new PlayerGraphicsComponent(),
                                     new PlayerInputComponent(),
                                     new PlayerPhysicsComponent(),
                                     new PlayerSoundComponent(),
                                     new PlayerGeneralComponent()
                                 };
            return new Player(components);
        }

        public static GameObject CreateCheckPoint()
        {
            var components = new List<IComponent> {new CheckPointGraphicsComponent()};
            return new GameObject(components);
        }

        public static GameObject CreateExit()
        {
            var components = new List<IComponent> {new ExitGraphicsComponent()};
            return new GameObject(components);
        }

        public static GameObject CreateKey()
        {
            var components = new List<IComponent> {new KeyComponent()};
            return new GameObject(components);
        }

        public static GameObject CreateLock()
        {
            var components = new List<IComponent> {new LockComponent()};
            return new GameObject(components);
        }

        public static GameObject CreateSmallTurret()
        {
            var components = new List<IComponent> {new SmallTurretComponent()};
            return new GameObject(components);
        }

        public static GameObject CreateMediumTurret()
        {
            var components = new List<IComponent> {new MediumTurretComponent()};
            return new GameObject(components);
        }

        public static GameObject CreateBoss1()
        {
            var components = new List<IComponent> {new Boss1Component()};
            return new GameObject(components);
        }

        public static GameObject CreateBossKiller()
        {
            var components = new List<IComponent> {new BossKillerComponent()};
            return new GameObject(components);
        }

        public static GameObject CreateSign()
        {
            var components = new List<IComponent>
                                 {
                                     new GraphicsComponent(
                                         VariableProvider.Game.Content.Load<Texture2D>(@"textures/entities/sign"))
                                 };
            return new GameObject(components);
        }
    }
}