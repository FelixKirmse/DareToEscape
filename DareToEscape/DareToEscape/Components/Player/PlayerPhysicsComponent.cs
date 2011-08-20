using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BlackDragonEngine.HelpMaps;
using BlackDragonEngine.Components;
using Microsoft.Xna.Framework.Input;
using BlackDragonEngine.Managers;
using DareToEscape.Helpers;

namespace DareToEscape.Components.Player
{
    class PlayerPhysicsComponent : PhysicsComponent
    {
        private float gravity;
        private float horiz;
        private bool onGround;
        private Rectangle collisionRectangle = new Rectangle(0, 0, 16, 24);
        private bool jumpThroughCheck = false;
        private bool inWater = false;

        public override void Update(GameObject obj)
        {
            Vector2 bottomLeftCorner, bottomRightCorner, topLeftCorner, topRightCorner, middleLeft, middleRight;            

            if (horiz != 0)
            {
                for (int i = 0; i < Math.Abs(horiz); ++i)
                {
                    if (inWater)
                        obj.Position.X += (horiz / Math.Abs(horiz)) / 2;
                    else
                        obj.Position.X += horiz / Math.Abs(horiz);

                    Rectangle CollisionRectangle = obj.GetCollisionRectangle(collisionRectangle);

                    bottomLeftCorner = TileMap.GetCellByPixel(new Vector2(CollisionRectangle.Left, CollisionRectangle.Bottom));
                    bottomRightCorner = TileMap.GetCellByPixel(new Vector2(CollisionRectangle.Right, CollisionRectangle.Bottom));

                    topLeftCorner = TileMap.GetCellByPixel(new Vector2(CollisionRectangle.Left, CollisionRectangle.Top));
                    topRightCorner = TileMap.GetCellByPixel(new Vector2(CollisionRectangle.Right, CollisionRectangle.Top));

                    middleLeft = TileMap.GetCellByPixel(new Vector2(CollisionRectangle.Left, (CollisionRectangle.Bottom + CollisionRectangle.Top) / 2));
                    middleRight = TileMap.GetCellByPixel(new Vector2(CollisionRectangle.Right, (CollisionRectangle.Bottom + CollisionRectangle.Top) / 2));

                    if (!TileMap.CellIsPassable(bottomLeftCorner) || !TileMap.CellIsPassable(bottomRightCorner) || !TileMap.CellIsPassable(topRightCorner) || !TileMap.CellIsPassable(topLeftCorner) || !TileMap.CellIsPassable(middleRight) || !TileMap.CellIsPassable(middleLeft))
                    {
                        if (inWater)
                            obj.Position.X -= (horiz / Math.Abs(horiz)) / 2;
                        else
                            obj.Position.X -= horiz / Math.Abs(horiz);                        
                        horiz = 0;
                        break;
                    }
                }
            }            
            obj.Send<bool>("GRAPHICS_SET_ONGROUND", onGround);
        }

        private void gravityLoop(GameObject obj)
        {
            Vector2 bottomLeftCorner, bottomRightCorner, topLeftCorner, topRightCorner, middleTop, middleBottom;

            

            if (gravity != 0)
            {
                for (int i = 0; i < Math.Abs(gravity); ++i)
                {
                    Rectangle CollisionRectangle = obj.GetCollisionRectangle(collisionRectangle);
                    bool collisionWithSpecialBlock = false;

                    bottomLeftCorner = TileMap.GetCellByPixel(new Vector2(CollisionRectangle.Left, CollisionRectangle.Bottom + 1));
                    bottomRightCorner = TileMap.GetCellByPixel(new Vector2(CollisionRectangle.Right, CollisionRectangle.Bottom + 1));

                    topLeftCorner = TileMap.GetCellByPixel(new Vector2(CollisionRectangle.Left, CollisionRectangle.Top - 1));
                    topRightCorner = TileMap.GetCellByPixel(new Vector2(CollisionRectangle.Right, CollisionRectangle.Top - 1));

                    middleTop = TileMap.GetCellByPixel(new Vector2((CollisionRectangle.Left + CollisionRectangle.Right)/2, CollisionRectangle.Top - 1));
                    middleBottom = TileMap.GetCellByPixel(new Vector2((CollisionRectangle.Left + CollisionRectangle.Right) / 2, CollisionRectangle.Bottom + 1));

                    if (jumpThroughCheck)
                    {                        
                        List<string> codePartsLeft = TileMap.GetCellCodes(bottomLeftCorner);
                        List<string> codePartsRight = TileMap.GetCellCodes(bottomRightCorner);
                        List<string> codePartsCenter = TileMap.GetCellCodes(middleBottom);

                        if (codePartsLeft.Contains("JUMPTHROUGH") || codePartsRight.Contains("JUMPTHROUGH") || codePartsCenter.Contains("JUMPTHROUGH"))
                            collisionWithSpecialBlock = true;                        
                    }

                    if (gravity > 0 && (!TileMap.CellIsPassable(bottomLeftCorner) || !TileMap.CellIsPassable(bottomRightCorner) || !TileMap.CellIsPassable(middleBottom) || collisionWithSpecialBlock))
                    {
                        gravity = 0;                        
                        obj.Send<int>("INPUT_SET_JUMPCOUNT", 0);
                        obj.Send<bool>("INPUT_SET_ONGROUND", true);
                        onGround = true;
                        break;
                    }

                    if (gravity < 0 && (!TileMap.CellIsPassable(topLeftCorner) || !TileMap.CellIsPassable(topRightCorner) || !TileMap.CellIsPassable(middleTop)))
                    {
                        gravity = 0;
                        obj.Position.Y += 1;
                        break;
                    }

                    if (gravity > 0)
                    {
                        if (inWater)
                            obj.Position.Y += .5f;
                        else
                            obj.Position.Y += 1;

                        obj.Send("GRAPHICS_PLAYANIMATION", "JumpDown");
                    }

                    if (gravity < 0)
                    {
                        if (inWater)
                            obj.Position.Y -= .5f;
                        else
                            obj.Position.Y -= 1;
                        obj.Send("GRAPHICS_PLAYANIMATION", "JumpUp");
                    }
                }
            }
            if(inWater)
                obj.Send<int>("INPUT_SET_JUMPCOUNT", 0);
            jumpThroughCheck = false;
            inWater = false;
            obj.Send<float>("INPUT_SET_GRAVITY", gravity);            
        }

        public override void Receive<T>(string message, T obj)
        {
            string[] messageParts = message.Split('_');

            if (messageParts[0] == "PHYSICS")
            {
                if (messageParts[1] == "SET")
                {
                    switch (messageParts[2])
                    {
                        case "GRAVITY":
                            if (obj is float)
                                gravity = (float)(object)obj;
                            break;

                        case "HORIZ":
                            if (obj is float)
                                horiz = (float)(object)obj;
                            break;  
                      
                        case "ONGROUND":
                            if (obj is bool)
                                onGround = (bool)(object)obj;
                            break;

                        case "JUMPTHROUGHCHECK":
                            if (obj is bool)
                                jumpThroughCheck = (bool)(object)obj;
                            break;

                        case "INWATER":
                            if (obj is bool)
                                inWater = (bool)(object)obj;
                            break;
                    }
                }
                if (messageParts[1] == "RUN")
                {
                    if (messageParts[2] == "GRAVITYLOOP")
                    {
                        gravityLoop((GameObject)(object)obj);
                    }
                }
            }
        }
    }
}
