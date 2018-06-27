using System.Collections.Generic;
using BlackDragonEngine.Components;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Providers;
using BlackDragonEngine.TileEngine;
using Microsoft.Xna.Framework;

namespace DareToEscape.Entities
{
    internal sealed class Player : GameObject
    {
        private const float MoveSpeed = 2f;
        private const ushort MaxJumpCount = 2;
        private const float JumpForce = -5f;
        private const float Gravity = .5f;
        private const float MaxGravity = 5f;
        private const float MinGravity = -10f;
        private readonly TileMap<Map<TileCode>, TileCode> _tileMap;
        private ushort _jumpCount;
        private int _lastSlopeX;
        private int _lastSlopeY;

        public Player(List<IComponent> components)
            : base(components)
        {
            collisionCircle = new BCircle(6, 9, 4);
            collisionRectangle = new Rectangle(7, 5, 10, 18);
            _tileMap = TileMap<Map<TileCode>, TileCode>.GetInstance();
        }

        public BCircle PlayerBulletCollisionCircle => CollisionCircle;

        public Vector2 PlayerBulletCollisionCircleCenter => CollisionCircle.Position;

        public static float PlayerPosX => ((Player) VariableProvider.CurrentPlayer).PlayerBulletCollisionCircleCenter.X;

        public static float PlayerPosY => ((Player) VariableProvider.CurrentPlayer).PlayerBulletCollisionCircleCenter.Y;

        public override void Update()
        {
            Velocity.X = 0f;
            if (InputMapper.Right)
            {
                Velocity.X += MoveSpeed;
                Send("GRAPHICS_SET_FLIPPED", false);
            }

            if (InputMapper.Left)
            {
                Velocity.X -= MoveSpeed;
                Send("GRAPHICS_SET_FLIPPED", true);
            }

            if (InputMapper.StrictJump && _jumpCount < MaxJumpCount)
            {
                ++_jumpCount;
                Velocity.Y = JumpForce;
                Velocity.Y = Velocity.Y < MinGravity ? MinGravity : Velocity.Y;
            }

            CollisionChecks();
            base.Update();
        }

        private void CollisionChecks()
        {
            var rect = CollisionRectangle;
            if (Velocity.Y > 0)
            {
                int slopeTileCoordX, slopeTileCoordY;
                var slopeCheckPointX = rect.Center.X + (int) Velocity.X;
                if (SlopeCollision(slopeCheckPointX, rect.Bottom, out slopeTileCoordX, out slopeTileCoordY))
                {
                    Position.X += Velocity.X;
                    if (Velocity.X < 0)
                        Send("GRAPHICS_SET_FLIPPED", true);
                    if (Velocity.X != 0f)
                        Send("GRAPHICS_PLAYANIMATION", "Walk");
                    _jumpCount = 0;
                    _lastSlopeX = slopeTileCoordX;
                    _lastSlopeY = slopeTileCoordY;
                    return;
                }

                //we're not on a slope this frame - check if we left a slope
                //-1 ... we didn't move from slope to slope
                //0 ... we left a slope after moving down
                //1 ... we left a slope after moving up
                var moveType = -1;
                var cellCodes = _tileMap.GetCellCodes(_lastSlopeX, _lastSlopeY);
                if (cellCodes.Contains(new TileCode(TileCodes.RightSlope)))
                    moveType = Velocity.X > 0 ? 0 : 1;
                else if (cellCodes.Contains(new TileCode(TileCodes.LeftSlope))) moveType = Velocity.X < 0 ? 0 : 1;

                if (moveType != -1)
                {
                    int slopeCheckPointY;
                    rect = CollisionRectangle;
                    if (moveType == 1)
                    {
                        Position.Y = slopeTileCoordY * _tileMap.TileHeight - rect.Height - 1 - collisionRectangle.Y;
                        slopeCheckPointY = CollisionRectangle.Y + rect.Height;
                    }
                    else
                    {
                        Position.Y = (slopeTileCoordY + 1) * _tileMap.TileHeight - rect.Height - 1 -
                                     collisionRectangle.Y;
                        slopeCheckPointY = CollisionRectangle.Y + rect.Height + _tileMap.TileHeight;
                    }

                    if (SlopeCollision(slopeCheckPointX, slopeCheckPointY, out slopeTileCoordX, out slopeTileCoordY))
                    {
                        Position.X += Velocity.X;
                        if (Velocity.X < 0)
                            Send("GRAPHICS_SET_FLIPPED", true);
                        if (Velocity.X != 0f)
                            Send("GRAPHICS_PLAYANIMATION", "Walk");
                        _jumpCount = 0;
                        _lastSlopeX = slopeTileCoordX;
                        _lastSlopeY = slopeTileCoordY;
                        return;
                    }
                }
            }

            AxisAxisCollision();
        }

        private bool SlopeCollision(int checkX, int checkY, out int slopeTileCoordX, out int slopeTileCoordY)
        {
            var rect = CollisionRectangle;
            slopeTileCoordX = _tileMap.GetCellByPixelX(checkX);
            slopeTileCoordY = _tileMap.GetCellByPixelY(checkY);
            var cellCodes = _tileMap.GetCellCodes(slopeTileCoordX, slopeTileCoordY);
            if (cellCodes.Contains(new TileCode(TileCodes.RightSlope)))
            {
                Position.Y = (slopeTileCoordY + 1) * _tileMap.TileHeight -
                             (_tileMap.TileWidth - checkX % _tileMap.TileWidth) - rect.Height - 1 -
                             collisionRectangle.Y;
                return true;
            }

            if (cellCodes.Contains(new TileCode(TileCodes.LeftSlope)))
            {
                Position.Y = (slopeTileCoordY + 1) * _tileMap.TileHeight - checkX % _tileMap.TileWidth - rect.Height -
                             1 -
                             collisionRectangle.Y;
                return true;
            }

            return false;
        }

        private void AxisAxisCollision()
        {
            int tileCoord;
            if (Velocity.X > 0f)
            {
                if (VerticalCollision(true, out tileCoord))
                    Position.X = tileCoord * _tileMap.TileWidth - collisionRectangle.Width - 1 - collisionRectangle.X;
                else
                    Position.X += Velocity.X;
                Send("GRAPHICS_PLAYANIMATION", "Walk");
            }
            else if (Velocity.X < 0f)
            {
                if (VerticalCollision(false, out tileCoord))
                    Position.X = (tileCoord + 1) * _tileMap.TileWidth + 1 - collisionRectangle.X;
                else
                    Position.X += Velocity.X;
                Send("GRAPHICS_PLAYANIMATION", "Walk");
            }
            else
            {
                Send("GRAPHICS_PLAYANIMATION", "Idle");
            }

            if (Velocity.Y < 0f)
            {
                if (HorizontalCollision(false, out tileCoord))
                {
                    Position.Y = (tileCoord + 1) * _tileMap.TileHeight - collisionRectangle.Y;
                    Velocity.Y = 0f;
                }
                else
                {
                    Position.Y += Velocity.Y;
                    Velocity.Y += Velocity.Y < Gravity ? InputMapper.Jump ? Gravity / 2f : Gravity : 0f;
                }

                Send("GRAPHICS_PLAYANIMATION", "JumpUp");
            }
            else
            {
                if (HorizontalCollision(true, out tileCoord))
                {
                    Position.Y = tileCoord * _tileMap.TileHeight - collisionRectangle.Height - 1 - collisionRectangle.Y;
                    Velocity.Y = 1f;
                    _jumpCount = 0;
                    Send("GRAPHICS_SET_ONGROUND", true);
                }
                else
                {
                    Position.Y += Velocity.Y;
                    Velocity.Y += Velocity.Y < MaxGravity ? Gravity : 0f;
                    Send("GRAPHICS_PLAYANIMATION", "JumpDown");
                }
            }

            _lastSlopeX = _tileMap.GetCellByPixelX(CollisionRectangle.Center.X);
            _lastSlopeY = _tileMap.GetCellByPixelY(CollisionRectangle.Y + collisionRectangle.Height);
        }

        private bool VerticalCollision(bool movingRight, out int tileCoordX)
        {
            float offset = movingRight ? collisionRectangle.Width : 0;
            offset += Velocity.X;
            var rect = CollisionRectangle;
            var tileYPixels = rect.Y - rect.Y % _tileMap.TileHeight;
            tileCoordX = _tileMap.GetCellByPixelX(rect.X + offset);
            var tileCoordY = _tileMap.GetCellByPixelY(tileYPixels);
            while (tileYPixels <= rect.Bottom)
            {
                if (!_tileMap.CellIsPassable(tileCoordX, tileCoordY))
                    return true;
                ++tileCoordY;
                tileYPixels += _tileMap.TileHeight;
            }

            return false;
        }

        private bool HorizontalCollision(bool movingDown, out int tileCoordY)
        {
            float offset = movingDown ? collisionRectangle.Height : 0;
            offset += Velocity.Y;
            var rect = CollisionRectangle;
            var tileXPixels = rect.X - rect.X % _tileMap.TileWidth;
            tileCoordY = _tileMap.GetCellByPixelY(rect.Y + offset);
            var tileCoordX = _tileMap.GetCellByPixelX(tileXPixels);
            while (tileXPixels <= rect.Right)
            {
                if (!_tileMap.CellIsPassable(tileCoordX, tileCoordY))
                    return true;
                ++tileCoordX;
                tileXPixels += _tileMap.TileWidth;
            }

            return false;
        }
    }
}