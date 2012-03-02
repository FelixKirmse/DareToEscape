using System;
using System.Collections.Generic;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.TileEngine;
using Microsoft.Xna.Framework;

namespace BlackDragonEngine.Components
{
    /// <summary>
    ///   Component to handle waypoint-movement for entities
    /// </summary>
    public class WaypointComponent : PhysicsComponent
    {
        /// <summary>
        ///   Speed at which to move the entity towards the current goal
        /// </summary>
        private const float Speed = 1;

        private readonly TileMap<Map<string>, string> _tileMap = TileMap<Map<string>, string>.GetInstance();

        /// <summary>
        ///   Disregard this, WTF?!
        /// </summary>
        protected Rectangle CollisionRectangle = new Rectangle(2, 14, 12, 12);

        /// <summary>
        ///   The waypoint that needs to be reached to finish the path
        /// </summary>
        protected Vector2 CurrentGoal;

        /// <summary>
        ///   The current path to the next waypoint
        /// </summary>
        protected List<Vector2> CurrentPath;

        /// <summary>
        ///   The current waypoint to head to
        /// </summary>
        protected Vector2 CurrentWaypoint;

        /// <summary>
        ///   The current direction to head to
        /// </summary>
        protected Vector2 Direction;

        /// <summary>
        ///   The current ObjectState
        /// </summary>
        protected ObjectStates ObjectState;

        /// <summary>
        ///   The current path index
        /// </summary>
        protected int PathIndex;

        /// <summary>
        ///   The fuck is this?
        /// </summary>
        protected bool SetRectangle = true;

        /// <summary>
        ///   The current waypoint index
        /// </summary>
        protected int WaypointIndex;

        /// <summary>
        ///   A list of waypoints
        /// </summary>
        protected List<Vector2> waypoints;

        /// <summary>
        ///   Updates the given object
        /// </summary>
        /// <param name="obj"> The object to update </param>
        public override void Update(GameObject obj)
        {
            if (SetRectangle)
            {
                SetRectangle = false;
                obj.CollisionRectangle = CollisionRectangle;
            }
            Vector2 collisionCenter = obj.RectCollisionCenter;

            if (ObjectState == ObjectStates.IDLE)
            {
                IDLEUpdate(collisionCenter, obj);
            }

            if (ObjectState == ObjectStates.Walking)
            {
                WalkingUpdate(collisionCenter, obj);
            }
        }

        /// <summary>
        ///   The routine that runs when the ObjectState is idle
        /// </summary>
        /// <param name="collisionCenter"> The objects collisioncenter </param>
        /// <param name="obj"> The object to update </param>
        protected virtual void IDLEUpdate(Vector2 collisionCenter, GameObject obj)
        {
            do
            {
                CurrentWaypoint = GetNextWaypoint();
                Coords coordCell = _tileMap.GetCellByPixel(collisionCenter);
                CurrentPath = PathFinder<Map<string>, string>.FindReducedPath(coordCell,
                                                                              CurrentWaypoint, _tileMap);
            } while (CurrentPath == null);
            PathIndex = 0;
            CurrentGoal = CurrentPath[PathIndex];
            ChangeDirection(collisionCenter, obj);
            ObjectState = ObjectStates.Walking;
        }

        /// <summary>
        ///   Changes the direction to head to
        /// </summary>
        /// <param name="collisionCenter"> The objects collisioncenter </param>
        /// <param name="obj"> The object to update </param>
        protected void ChangeDirection(Vector2 collisionCenter, GameObject obj)
        {
            Direction = _tileMap.GetCellCenter(CurrentGoal) - collisionCenter;
            if (Direction != Vector2.Zero)
                Direction.Normalize();
            DetermineAnimation(obj);
        }

        /// <summary>
        ///   The routine that runs when the ObjectState is walking
        /// </summary>
        /// <param name="collisionCenter"> The objects collisioncenter </param>
        /// <param name="obj"> The object to update </param>
        protected virtual void WalkingUpdate(Vector2 collisionCenter, GameObject obj)
        {
            obj.Position += Speed*Direction;

            if (Vector2.Distance(collisionCenter, _tileMap.GetCellCenter(CurrentGoal)) <= Speed*2 &&
                PathIndex != CurrentPath.Count - 1)
            {
                CurrentGoal = CurrentPath[++PathIndex];
                ChangeDirection(collisionCenter, obj);
            }

            if (Vector2.Distance(collisionCenter, _tileMap.GetCellCenter(CurrentWaypoint)) <= Speed*2)
            {
                ObjectState = ObjectStates.IDLE;
            }
        }

        /// <summary>
        ///   Returns the next waypoint to generate a path for
        /// </summary>
        /// <returns> Waypoint to generate a path for </returns>
        protected virtual Vector2 GetNextWaypoint()
        {
            if (WaypointIndex == waypoints.Count - 1)
            {
                WaypointIndex = 0;
                return waypoints[WaypointIndex];
            }

            return waypoints[++WaypointIndex];
        }

        /// <summary>
        ///   Determines the animation based on the direction
        /// </summary>
        /// <param name="obj"> The object to update </param>
        protected virtual void DetermineAnimation(GameObject obj)
        {
            string animation = Direction.Y > 0 ? "WalkDown" : "WalkUp";
            bool flipped = Direction.X < 0;

            if (Direction.X != 0)
            {
                if (Direction.Y == 0)
                {
                    animation = "WalkSide";
                }
                else
                {
                    if (Math.Abs(Direction.X) > .85f)
                        animation = "WalkSide";
                    else if (Math.Abs(Direction.X) > .15f)
                        animation += "Side";
                }
            }
            obj.Send("GRAPHICS_SET_FLIPPED", flipped);
            obj.Send("GRAPHICS_PLAYANIMATION", animation);
        }

        #region Nested type: ObjectStates

        /// <summary>
        ///   The possible states the object can be in
        /// </summary>
        protected enum ObjectStates
        {
            IDLE,
            Walking
        };

        #endregion
    }
}