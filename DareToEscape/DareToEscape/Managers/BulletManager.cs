using System.Collections.Generic;
using DareToEscape.Entities;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DareToEscape.Managers
{
    class BulletManager : GameComponent
    {
        private readonly List<Bullet> bullets = new List<Bullet>();

        public BulletManager(Game game)
            : base(game)
        {             
        }

        public void ClearAllBullets()
        {
            bullets.Clear();
        }

        public void AddBullet(Bullet bullet)
        {
            bullets.Add(bullet);
        }

        public override void Update(GameTime gameTime)
        {
            if (StateManager.GameState == GameStates.Ingame || StateManager.GameState == GameStates.Editor && !StateManager.GamePaused)
            {
                foreach (var bullet in bullets)
                {
                    bullet.Update();
                }
                bullets.RemoveAll(s => !s.Active);                
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (StateManager.GameState == GameStates.Ingame || StateManager.GameState == GameStates.Editor)
            {
                foreach (Bullet bullet in bullets)
                {
                    bullet.Draw(spriteBatch);
                }
            }
        }        
    }
}
