using BlackDragonEngine.Components;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Helpers;

namespace DareToEscape.Components.PlayerComponents
{
    internal class PlayerInputComponent : InputComponent
    {
        private float _gravity;
        private int _jumpCount;
        private bool _onGround;

        public override void Update(GameObject obj)
        {
            if (!InputMapper.Jump && _gravity < 0)
            {
                _gravity += .45f;
            }

            if (_gravity < 10)
                _gravity += 0.5f;

            if (InputMapper.StrictJump && _jumpCount == 1 && _gravity > -5)
            {
                _gravity = -8;
                _jumpCount = 2;
            }
            obj.Send("PHYSICS_SET_GRAVITY", _gravity);
            obj.Send("PHYSICS_RUN_GRAVITYLOOP", obj);

            if (InputMapper.StrictJump && _onGround)
            {
                _gravity = -10;
                _jumpCount = 1;
                _onGround = false;
                obj.Send("PHYSICS_SET_ONGROUND", false);
            }

            if (_gravity > 0 && _jumpCount != 2)
            {
                _jumpCount = 1;
            }


            if (InputMapper.Left)
            {
                obj.Send<float>("PHYSICS_SET_HORIZ", -2);
                if (_onGround)
                {
                    obj.Send("GRAPHICS_PLAYANIMATION", "Walk");
                }
                obj.Send("GRAPHICS_SET_FLIPPED", true);
            }
            else if (InputMapper.Right)
            {
                obj.Send<float>("PHYSICS_SET_HORIZ", 2);
                if (_onGround)
                {
                    obj.Send("GRAPHICS_PLAYANIMATION", "Walk");
                }
                obj.Send("GRAPHICS_SET_FLIPPED", false);
            }
            else
            {
                obj.Send<float>("PHYSICS_SET_HORIZ", 0);
                if (_onGround)
                {
                    obj.Send("GRAPHICS_PLAYANIMATION", "Idle");
                }
            }

            obj.Send("PHYSICS_SET_FOCUSED", InputMapper.TriggeredAction("Focus"));
            obj.Send("GRAPHICS_SET_FOCUSED", InputMapper.TriggeredAction("Focus"));
        }

        public override void Receive<T>(string message, T obj)
        {
            string[] messageParts = message.Split('_');

            if (messageParts[0] == "INPUT")
            {
                if (messageParts[1] == "SET")
                {
                    switch (messageParts[2])
                    {
                        case "GRAVITY":
                            if (obj is float)
                                _gravity = (float) (object) obj;
                            break;

                        case "ONGROUND":
                            if (obj is bool)
                                _onGround = (bool) (object) obj;
                            break;

                        case "JUMPCOUNT":
                            if (obj is int)
                                _jumpCount = (int) (object) obj;
                            break;
                    }
                }
            }
        }
    }
}