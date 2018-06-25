using BlackDragonEngine.Components;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Providers;
using DareToEscape.Providers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape.Components.Entities
{
    internal class BossKillerComponent : GraphicsComponent
    {
        public bool enabled = true;
        public bool setRectangle = true;

        public BossKillerComponent()
        {
            Texture = VariableProvider.Game.Content.Load<Texture2D>("textures/entities/bosskiller");
        }

        public override void Update(GameObject obj)
        {
            if (enabled)
            {
                if (setRectangle)
                {
                    obj.CollisionRectangle = new Rectangle(6, 5, 39, 39);
                }

                if (VariableProvider.CurrentPlayer.CollisionRectangle.Intersects(obj.CollisionRectangle))
                {
                    enabled = false;
                    foreach (var boss in GameVariableProvider.Bosses)
                        boss.Send<string>("INACTIVE", null);
                }
            }
        }

        public override void Draw(GameObject obj)
        {
            if (enabled)
                base.Draw(obj);
        }
    }
}