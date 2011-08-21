using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BlackDragonEngine.Components;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Providers;

namespace BlackDragonEngine.Entities
{
    public class GameObject
    {
        public Vector2 Velocity;
        protected Vector2 position;
        protected Rectangle collisionRectangle;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        
        public Rectangle CollisionRectangle 
        {
            get 
            {
                return new Rectangle((int)Position.X + collisionRectangle.X, (int)Position.Y + collisionRectangle.Y, collisionRectangle.Width, collisionRectangle.Height);
            }
            set { collisionRectangle = value; }             
        }

        public Vector2 CollisionCenter
        {
            get 
            {
                return new Vector2((CollisionRectangle.Right + CollisionRectangle.Left) / 2, (CollisionRectangle.Bottom + CollisionRectangle.Top) / 2);
            }
        }

        public Rectangle GetCustomCollisionRectangle(Vector2 customPosition)
        {
            return new Rectangle((int)customPosition.X + collisionRectangle.X, (int)customPosition.Y + collisionRectangle.Y, collisionRectangle.Width, collisionRectangle.Height);
        }

        protected List<Component> components = new List<Component>();

        public Vector2 ScreenPosition
        {
            get 
            {
                return ShortcutProvider.Vector2Point(Camera.WorldToScreen(Position));
            }
        }

        public GameObject()
        { 
        }

        public GameObject(List<Component> components)
        {
            this.components = components;
        }

        public virtual void Update()
        {
            foreach (Component component in components)
            {
                component.Update(this);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (Component component in components)
            {
                if (component is GraphicsComponent)
                {
                    ((GraphicsComponent)component).Draw(this, spriteBatch);
                }
            }
        }        

        public void Send<T>(string Message, T obj)
        {
            foreach (Component component in components)
            {
                component.Receive<T>(Message, obj);
            }
        }        
    }
}
