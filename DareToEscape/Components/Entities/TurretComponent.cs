using System;
using System.Collections.Generic;
using BlackDragonEngine.Components;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Providers;
using BlackDragonEngine.TileEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DareToEscape.Components.Entities
{
    internal class TurretComponent : GraphicsComponent
    {
        private float _rotation;
        public Vector2 BulletOrigin;

        private Vector2 RotationOrigin => new Vector2(Texture.Width / 2, Texture.Height / 2);

        public override void Update(GameObject obj)
        {
            if (ShootCondition(VariableProvider.CurrentPlayer.RectCollisionCenter, obj))
                if (!VariableProvider.ScriptEngine.IsScriptRunning(ShootBehavior))
                    VariableProvider.ScriptEngine.ExecuteScript(ShootBehavior);
        }

        public override void Draw(GameObject obj)
        {
            SpriteBatch.Draw(
                Texture,
                Camera.WorldToScreen(obj.Position + RotationOrigin),
                null,
                Color.White,
                _rotation,
                RotationOrigin,
                1f,
                SpriteEffects.None,
                DrawDepth);
        }

        protected virtual bool ShootCondition(Vector2 playerPosition, GameObject turret)
        {
            var startX = Camera.Position.X;
            var endX = Camera.Position.X + Camera.ViewPortWidth;
            var startY = Camera.Position.Y;
            var endY = Camera.Position.Y + Camera.ViewPortHeight;

            var pos = turret.Position;

            if (pos.X >= startX && pos.X <= endX && pos.Y >= startY && pos.Y <= endY)
            {
                var tileMap = TileMap<Map<TileCode>, TileCode>.GetInstance();
                var direction = playerPosition - BulletOrigin;
                direction /= tileMap.TileWidth * 32;
                var particlePosition = BulletOrigin;

                while (particlePosition != playerPosition)
                {
                    if (!tileMap.CellIsPassableByPixel(particlePosition))
                        return false;
                    particlePosition += direction;
                }

                return true;
            }

            return false;
        }

        protected virtual IEnumerator<int> ShootBehavior(params float[] parameters)
        {
            throw new NotImplementedException();
        }

        protected void SetDown(GameObject obj)
        {
            if (Texture.Height != 16)
                obj.Position += new Vector2(0, -Texture.Height / 2);
            if (Texture.Height == 64)
                obj.Position += new Vector2(0, -Texture.Height / 4);
            BulletOrigin = obj.Position + RotationOrigin + new Vector2(0, -Texture.Height / 2);
            _rotation = MathHelper.ToRadians(180);
        }

        protected void SetLeft(GameObject obj)
        {
            BulletOrigin = obj.Position + RotationOrigin + new Vector2(Texture.Width / 2, 0);
            _rotation = MathHelper.ToRadians(270);
        }

        protected void SetRight(GameObject obj)
        {
            if (Texture.Height != 16)
                obj.Position += new Vector2(-Texture.Width / 2, 0);
            if (Texture.Height == 64)
                obj.Position += new Vector2(-Texture.Width / 4, 0);
            BulletOrigin = obj.Position + RotationOrigin + new Vector2(-Texture.Width / 2, 0);
            _rotation = MathHelper.ToRadians(90);
        }

        protected void SetUp(GameObject obj)
        {
            BulletOrigin = obj.Position + RotationOrigin + new Vector2(0, Texture.Height / 2);
            _rotation = MathHelper.ToRadians(0);
        }

        public override void Receive<T>(string message, T obj)
        {
            var messageParts = message.Split('_');
            if (messageParts[0] == "SET")
                switch (messageParts[1])
                {
                    case "UP":
                        SetUp((GameObject) (object) obj);
                        break;
                    case "DOWN":
                        SetDown((GameObject) (object) obj);
                        break;
                    case "LEFT":
                        SetLeft((GameObject) (object) obj);
                        break;
                    case "RIGHT":
                        SetRight((GameObject) (object) obj);
                        break;
                }
        }
    }
}