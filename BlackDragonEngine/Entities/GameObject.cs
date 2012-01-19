using System.Collections.Generic;
using BlackDragonEngine.Components;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework;

namespace BlackDragonEngine.Entities
{
    /// <summary>
    ///   Barebone GameObject class
    /// </summary>
    public class GameObject : IGameObject
    {
        /// <summary>
        ///   The list of components this object uses
        /// </summary>
        protected readonly List<IComponent> components = new List<IComponent>();

        /// <summary>
        ///   Velocity of the Object [DEPRECATED]
        /// </summary>
        public Vector2 Velocity;

        /// <summary>
        ///   The bounding circle of the object with local position
        /// </summary>
        protected BCircle collisionCircle;

        /// <summary>
        ///   The collision rectangle of the object in local coordinates
        /// </summary>
        protected Rectangle collisionRectangle;

        /// <summary>
        ///   The current position of the Object
        /// </summary>
        protected Vector2 position;

        public GameObject()
        {
        }

        /// <summary>
        ///   Returns an Instance of a GameObject with the components specified in the parameter
        /// </summary>
        /// <param name = "components">The components the object should use</param>
        public GameObject(List<IComponent> components)
        {
            this.components = components;
        }

        /// <summary>
        ///   Returns an Instance of a GameObject with the component specified
        /// </summary>
        /// <param name = "component"></param>
        public GameObject(IComponent component)
        {
            components = new List<IComponent> {component};
        }

        /// <summary>
        ///   Gets or sets the position of the object
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        ///   Gets the collision rectangle of the object in world coordinates or sets the rectangle in local coordinates
        /// </summary>
        public Rectangle CollisionRectangle
        {
            get
            {
                return new Rectangle((int) Position.X + collisionRectangle.X, (int) Position.Y + collisionRectangle.Y,
                                     collisionRectangle.Width, collisionRectangle.Height);
            }
            set { collisionRectangle = value; }
        }

        /// <summary>
        ///   Gets the bounding circle of the object in world coordinates or sets the circle in local coordinates
        /// </summary>
        public BCircle CollisionCircle
        {
            get { return new BCircle(position + collisionCircle.Position, collisionCircle.Radius); }
            set { collisionCircle = value; }
        }

        public Vector2 BCircleLocalCenter
        {
            get { return collisionCircle.Position; }
        }

        /// <summary>
        ///   Gets the collision center of the object in world coordinates
        /// </summary>
        public Vector2 RectCollisionCenter
        {
            get
            {
                return new Vector2((CollisionRectangle.Right + CollisionRectangle.Left)/2,
                                   (CollisionRectangle.Bottom + CollisionRectangle.Top)/2);
            }
        }

        /// <summary>
        ///   Gets the collision center of the object in world coordinates
        /// </summary>
        public Vector2 CircleCollisionCenter
        {
            get { return position + collisionCircle.Position; }
        }

        /// <summary>
        ///   Position of the object in screencoordinates (used when drawing)
        /// </summary>
        public Vector2 ScreenPosition
        {
            get { return ShortCuts.Vector2Point(Camera.WorldToScreen(Position)); }
        }

        #region IGameObject Members

        public List<IComponent> Components
        {
            get { return components; }
        }

        /// <summary>
        ///   Updates all the components
        /// </summary>
        public virtual void Update()
        {
            foreach (var component in components)
            {
                component.Update(this);
            }
        }

        /// <summary>
        ///   Draws all drawable components
        /// </summary>
        public virtual void Draw()
        {
            foreach (var component in components)
            {
                if (component is GraphicsComponent)
                {
                    ((GraphicsComponent) component).Draw(this);
                }
            }
        }

        #endregion

        /// <summary>
        ///   [DEPRECATED] Returns a custom collision rectangle based on a position
        /// </summary>
        /// <param name = "customPosition"></param>
        /// <returns></returns>
        public Rectangle GetCustomCollisionRectangle(Vector2 customPosition)
        {
            return new Rectangle((int) customPosition.X + collisionRectangle.X,
                                 (int) customPosition.Y + collisionRectangle.Y, collisionRectangle.Width,
                                 collisionRectangle.Height);
        }

        /// <summary>
        ///   Used to send messages to all components
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "message">The message</param>
        /// <param name = "obj">An attachment</param>
        public void Send<T>(string message, T obj)
        {
            foreach (var component in components)
            {
                component.Receive(message, obj);
            }
        }
    }
}