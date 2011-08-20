using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.Entities;
using Microsoft.Xna.Framework;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.HelpMaps;


namespace BlackDragonEngine.Components
{
    public class WaypointComponent : PhysicsComponent
    {
        protected enum ObjectStates { IDLE, WALKING };
        protected ObjectStates objectState;
        protected List<Vector2> currentPath;
        protected List<Vector2> waypoints;

        protected Vector2 currentWaypoint;
        protected Vector2 currentGoal;
        protected int pathIndex = 0;
        protected int waypointIndex = 0;

        protected float speed = 1;
        protected Rectangle collisionRectangle = new Rectangle(2, 14, 12, 12);
        protected Vector2 direction;


        public override void Update(GameObject obj)
        {
            Vector2 collisionCenter = obj.GetCollisionCenter(collisionRectangle);

            if (objectState == ObjectStates.IDLE)
            {
                idleUpdate(collisionCenter, obj);
            }

            if (objectState == ObjectStates.WALKING)
            {
                walkingUpdate(collisionCenter, obj);
            }
        }

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

        protected void changeDirection(Vector2 collisionCenter, GameObject obj)
        {            
            direction = TileMap.GetCellCenter(currentGoal) - collisionCenter;            
            if (direction != Vector2.Zero)
                direction.Normalize();
            determineAnimation(obj);
        }

        protected virtual void walkingUpdate(Vector2 collisionCenter, GameObject obj)
        {    
            obj.Position += speed * direction;

            if (Vector2.Distance(collisionCenter, TileMap.GetCellCenter(currentGoal)) <= speed*2 && pathIndex != currentPath.Count - 1)
            {
                currentGoal = currentPath[++pathIndex];
                changeDirection(collisionCenter, obj);
            }

            if (Vector2.Distance(collisionCenter, TileMap.GetCellCenter(currentWaypoint)) <= speed*2)
            {
                objectState = ObjectStates.IDLE;
            }
        }

        protected virtual Vector2 getNextWaypoint()
        {
            if (waypointIndex == waypoints.Count - 1)
            {
                waypointIndex = 0;
                return waypoints[waypointIndex];
            }

            return waypoints[++waypointIndex];
        }

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
    }
}
