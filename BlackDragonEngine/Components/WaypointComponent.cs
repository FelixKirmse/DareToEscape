using System;
using System.Collections.Generic;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.TileEngine;
using Microsoft.Xna.Framework;

namespace BlackDragonEngine.Components
{
    /// <summary>
    /// Component to handle waypoint-movement for entities
    /// </summary>
    public class WaypointComponent : PhysicsComponent
    {
        /// <summary>
        /// Disregard this, WTF?!
        /// </summary>
        protected Rectangle collisionRectangle = new Rectangle(2, 14, 12, 12);

        /// <summary>
        /// The waypoint that needs to be reached to finish the path
        /// </summary>
        protected Vector2 currentGoal;

        /// <summary>
        /// The current path to the next waypoint
        /// </summary>
        protected List<Vector2> currentPath;

        /// <summary>
        /// The current waypoint to head to
        /// </summary>
        protected Vector2 currentWaypoint;

        /// <summary>
        /// The current direction to head to
        /// </summary>
        protected Vector2 direction;

        /// <summary>
        /// The current ObjectState
        /// </summary>
        protected ObjectStates objectState;

        /// <summary>
        /// The current path index
        /// </summary>
        protected int pathIndex;

        /// <summary>
        /// The fuck is this?
        /// </summary>
        protected bool setRectangle = true;

        /// <summary>
        /// Speed at which to move the entity towards the current goal
        /// </summary>
        protected float speed = 1;

        /// <summary>
        /// The current waypoint index
        /// </summary>
        protected int waypointIndex;

        /// <summary>
        /// A list of waypoints
        /// </summary>
        protected List<Vector2> waypoints;

        /// <summary>
        /// Updates the given object
        /// </summary>
        /// <param name="obj">The object to update</param>
        public override void Update(GameObject obj)
        {
            if (setRectangle)
            {
                setRectangle = false;
                obj.CollisionRectangle = collisionRectangle;
            }
            Vector2 collisionCenter = obj.RectCollisionCenter;

            if (objectState == ObjectStates.IDLE)
            {
                idleUpdate(collisionCenter, obj);
            }

            if (objectState == ObjectStates.WALKING)
            {
                walkingUpdate(collisionCenter, obj);
            }
        }

        /// <summary>
        /// The routine that runs when the ObjectState is idle
        /// </summary>
        /// <param name="collisionCenter">The objects collisioncenter</param>
        /// <param name="obj">The object to update</param>
        protected virtual void idleUpdate(Vector2 collisionCenter, GameObject obj)
        {
            do
            {
                currentWaypoint = getNextWaypoint();
                currentPath = PathFinder.FindReducedPath(TileMap.GetCellByPixel(collisionCenter), currentWaypoint);
            } while (currentPath == null);
            pathIndex = 0;
            currentGoal = currentPath[pathIndex];
            changeDirection(collisionCenter, obj);
            objectState = ObjectStates.WALKING;
        }

        /// <summary>
        /// Changes the direction to head to
        /// </summary>
        /// <param name="collisionCenter">The objects collisioncenter</param>
        /// <param name="obj">The object to update</param>
        protected void changeDirection(Vector2 collisionCenter, GameObject obj)
        {
            direction = TileMap.GetCellCenter(currentGoal) - collisionCenter;
            if (direction != Vector2.Zero)
                direction.Normalize();
            determineAnimation(obj);
        }

        /// <summary>
        /// The routine that runs when the ObjectState is walking
        /// </summary>
        /// <param name="collisionCenter">The objects collisioncenter</param>
        /// <param name="obj">The object to update</param>
        protected virtual void walkingUpdate(Vector2 collisionCenter, GameObject obj)
        {
            obj.Position += speed*direction;

            if (Vector2.Distance(collisionCenter, TileMap.GetCellCenter(currentGoal)) <= speed*2 &&
                pathIndex != currentPath.Count - 1)
            {
                currentGoal = currentPath[++pathIndex];
                changeDirection(collisionCenter, obj);
            }

            if (Vector2.Distance(collisionCenter, TileMap.GetCellCenter(currentWaypoint)) <= speed*2)
            {
                objectState = ObjectStates.IDLE;
            }
        }

        /// <summary>
        /// Returns the next waypoint to generate a path for
        /// </summary>
        /// <returns>Waypoint to generate a path for</returns>
        protected virtual Vector2 getNextWaypoint()
        {
            if (waypointIndex == waypoints.Count - 1)
            {
                waypointIndex = 0;
                return waypoints[waypointIndex];
            }

            return waypoints[++waypointIndex];
        }

        /// <summary>
        /// Determines the animation based on the direction
        /// </summary>
        /// <param name="obj">The object to update</param>
        protected virtual void determineAnimation(GameObject obj)
        {
            string animation = direction.Y > 0 ? "WalkDown" : "WalkUp";
            bool flipped = direction.X < 0;

            if (direction.X != 0)
            {
                if (direction.Y == 0)
                {
                    animation = "WalkSide";
                }
                else
                {
                    if (Math.Abs(direction.X) > .85f)
                        animation = "WalkSide";
                    else if (Math.Abs(direction.X) > .15f)
                        animation += "Side";
                }
            }
            obj.Send("GRAPHICS_SET_FLIPPED", flipped);
            obj.Send("GRAPHICS_PLAYANIMATION", animation);
        }

        #region Nested type: ObjectStates

        /// <summary>
        /// The possible states the object can be in 
        /// </summary>
        protected enum ObjectStates
        {
            IDLE,
            WALKING
        };

        #endregion
    }
}