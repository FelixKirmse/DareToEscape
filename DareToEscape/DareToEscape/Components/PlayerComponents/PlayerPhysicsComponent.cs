using System;
using System.Collections.Generic;
using BlackDragonEngine.Components;
using BlackDragonEngine.Entities;
using BlackDragonEngine.TileEngine;
using Microsoft.Xna.Framework;

namespace DareToEscape.Components.PlayerComponents
{
    internal class PlayerPhysicsComponent : PhysicsComponent
    {
        private readonly Rectangle _collisionRectangle = new Rectangle(0, 0, 16, 22);
        private bool _focused;
        private float _gravity;
        private float _horiz;
        private bool _inWater;
        private bool _jumpThroughCheck;

        private bool _noLeft;
        private bool _noRight;
        private bool _onGround;
        private bool _setRectangle = true;

        public override void Update(GameObject obj)
        {
            Vector2 wantedPosition = obj.Position;

            if (_setRectangle)
            {
                obj.CollisionRectangle = _collisionRectangle;
                _setRectangle = false;
            }

            if (_horiz != 0)
            {
                obj.Send("GRAPHICS_SET_ONGROUND", _onGround);
                if ((_horiz > 0 && _noRight) || (_horiz < 0 && _noLeft))
                {
                    _noRight = false;
                    _noLeft = false;
                    obj.Send("GRAPHICS_PLAYANIMATION", "Idle");
                    return;
                }
                for (int i = 0; i < Math.Abs(_horiz); ++i)
                {
                    if (_inWater || _focused)
                        wantedPosition.X += (_horiz/Math.Abs(_horiz))/2;
                    else
                        wantedPosition.X += _horiz/Math.Abs(_horiz);

                    Rectangle collisionRectangle = obj.GetCustomCollisionRectangle(wantedPosition);

                    Vector2 bottomLeftCorner = TileMap.GetCellByPixel(new Vector2(collisionRectangle.Left, collisionRectangle.Bottom));
                    Vector2 bottomRightCorner = TileMap.GetCellByPixel(new Vector2(collisionRectangle.Right, collisionRectangle.Bottom));

                    Vector2 topLeftCorner = TileMap.GetCellByPixel(new Vector2(collisionRectangle.Left, collisionRectangle.Top));
                    Vector2 topRightCorner = TileMap.GetCellByPixel(new Vector2(collisionRectangle.Right, collisionRectangle.Top));

                    Vector2 middleLeft = TileMap.GetCellByPixel(new Vector2(collisionRectangle.Left,
                                                                            (collisionRectangle.Bottom + collisionRectangle.Top)/2));
                    Vector2 middleRight = TileMap.GetCellByPixel(new Vector2(collisionRectangle.Right,
                                                                             (collisionRectangle.Bottom + collisionRectangle.Top)/2));

                    if (!TileMap.CellIsPassable(bottomLeftCorner) || !TileMap.CellIsPassable(bottomRightCorner) ||
                        !TileMap.CellIsPassable(topRightCorner) || !TileMap.CellIsPassable(topLeftCorner) ||
                        !TileMap.CellIsPassable(middleRight) || !TileMap.CellIsPassable(middleLeft))
                    {
                        if (_inWater || _focused)
                            wantedPosition.X -= (_horiz/Math.Abs(_horiz))/2;
                        else
                            wantedPosition.X -= _horiz/Math.Abs(_horiz);
                        _horiz = 0;
                        break;
                    }
                }
                obj.Position = wantedPosition;
            }
        }

        private void GravityLoop(GameObject obj)
        {
            Vector2 wantedPosition = obj.Position;

            if (_gravity != 0)
            {
                for (int i = 0; i < Math.Abs(_gravity); ++i)
                {
                    Rectangle collisionRectangle = obj.GetCustomCollisionRectangle(wantedPosition);
                    bool collisionWithSpecialBlock = false;

                    Vector2 bottomLeftCorner = TileMap.GetCellByPixel(new Vector2(collisionRectangle.Left, collisionRectangle.Bottom + 1));
                    Vector2 bottomRightCorner = TileMap.GetCellByPixel(new Vector2(collisionRectangle.Right, collisionRectangle.Bottom + 1));

                    Vector2 topLeftCorner = TileMap.GetCellByPixel(new Vector2(collisionRectangle.Left, collisionRectangle.Top - 1));
                    Vector2 topRightCorner = TileMap.GetCellByPixel(new Vector2(collisionRectangle.Right, collisionRectangle.Top - 1));

                    Vector2 middleTop = TileMap.GetCellByPixel(new Vector2((collisionRectangle.Left + collisionRectangle.Right)/2,
                                                                           collisionRectangle.Top - 1));
                    Vector2 middleBottom = TileMap.GetCellByPixel(new Vector2((collisionRectangle.Left + collisionRectangle.Right)/2,
                                                                              collisionRectangle.Bottom + 1));

                    if (_jumpThroughCheck)
                    {
                        List<string> codePartsLeft = TileMap.GetCellCodes(bottomLeftCorner);
                        List<string> codePartsRight = TileMap.GetCellCodes(bottomRightCorner);
                        List<string> codePartsCenter = TileMap.GetCellCodes(middleBottom);

                        if (codePartsLeft.Contains("JUMPTHROUGH") || codePartsRight.Contains("JUMPTHROUGH") ||
                            codePartsCenter.Contains("JUMPTHROUGH"))
                            collisionWithSpecialBlock = true;
                    }

                    if (_gravity > 0 &&
                        (!TileMap.CellIsPassable(bottomLeftCorner) || !TileMap.CellIsPassable(bottomRightCorner) ||
                         !TileMap.CellIsPassable(middleBottom) || collisionWithSpecialBlock))
                    {
                        _gravity = 0;
                        obj.Send("INPUT_SET_JUMPCOUNT", 0);
                        obj.Send("INPUT_SET_ONGROUND", true);
                        _onGround = true;
                        break;
                    }

                    if (_gravity < 0 &&
                        (!TileMap.CellIsPassable(topLeftCorner) || !TileMap.CellIsPassable(topRightCorner) ||
                         !TileMap.CellIsPassable(middleTop)))
                    {
                        _gravity = 0;
                        wantedPosition.Y += .5f;
                        break;
                    }

                    if (_gravity > 0)
                    {
                        if (_inWater)
                            wantedPosition.Y += .25f;
                        else
                            wantedPosition.Y += .5f;

                        obj.Send("GRAPHICS_PLAYANIMATION", "JumpDown");
                    }

                    if (_gravity < 0)
                    {
                        if (_inWater)
                            wantedPosition.Y -= .25f;
                        else
                            wantedPosition.Y -= .5f;
                        obj.Send("GRAPHICS_PLAYANIMATION", "JumpUp");
                    }
                }
            }
            if (_inWater)
                obj.Send("INPUT_SET_JUMPCOUNT", 0);
            _jumpThroughCheck = false;
            _inWater = false;
            obj.Send("INPUT_SET_GRAVITY", _gravity);

            obj.Position = wantedPosition;
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
                                _gravity = (float) (object) obj;
                            break;

                        case "HORIZ":
                            if (obj is float)
                                _horiz = (float) (object) obj;
                            break;

                        case "ONGROUND":
                            if (obj is bool)
                                _onGround = (bool) (object) obj;
                            break;

                        case "JUMPTHROUGHCHECK":
                            if (obj is bool)
                                _jumpThroughCheck = (bool) (object) obj;
                            break;

                        case "INWATER":
                            if (obj is bool)
                                _inWater = (bool) (object) obj;
                            break;

                        case "NORIGHT":
                            if (obj is bool)
                                _noRight = (bool) (object) obj;
                            break;

                        case "NOLEFT":
                            if (obj is bool)
                                _noLeft = (bool) (object) obj;
                            break;

                        case "FOCUSED":
                            if (obj is bool)
                                _focused = (bool) (object) obj;
                            break;
                    }
                }
                if (messageParts[1] == "RUN")
                {
                    if (messageParts[2] == "GRAVITYLOOP")
                    {
                        GravityLoop((GameObject) (object) obj);
                    }
                }
            }
        }
    }
}