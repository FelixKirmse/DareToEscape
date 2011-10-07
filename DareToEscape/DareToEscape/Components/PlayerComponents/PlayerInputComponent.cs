using BlackDragonEngine.Entities;
using BlackDragonEngine.Components;
using BlackDragonEngine.Helpers;


namespace DareToEscape.Components.PlayerComponents
{
    class PlayerInputComponent : InputComponent
    {

        private float gravity;        
        private bool onGround;
        private int jumpCount;        

        public override void Update(GameObject obj)
        {            
            if (!InputMapper.Jump && gravity < 0)
            {
                gravity += .45f;                
            }

            if (gravity < 10)
                gravity += 0.5f;

            if (InputMapper.StrictJump && jumpCount == 1 && gravity > -5)
            {
                gravity = -8;
                jumpCount = 2;
            }
            obj.Send<float>("PHYSICS_SET_GRAVITY", gravity);
            obj.Send<GameObject>("PHYSICS_RUN_GRAVITYLOOP", obj);

            if (InputMapper.StrictJump && onGround)
            {
                gravity = -10;
                jumpCount = 1;
                onGround = false;
                obj.Send<bool>("PHYSICS_SET_ONGROUND", false);
            }

            if (gravity > 0 && jumpCount != 2)
            {
                jumpCount = 1;
            }


            if (InputMapper.Left)
            {
                obj.Send<float>("PHYSICS_SET_HORIZ", -5);
                if (onGround)
                {
                    obj.Send("GRAPHICS_PLAYANIMATION", "Walk");
                }
                obj.Send<bool>("GRAPHICS_SET_FLIPPED", true);
            }
            else if (InputMapper.Right)
            {
                obj.Send<float>("PHYSICS_SET_HORIZ", 5);
                if (onGround)
                {
                    obj.Send("GRAPHICS_PLAYANIMATION", "Walk");
                }
                obj.Send<bool>("GRAPHICS_SET_FLIPPED", false);
            }
            else
            {
                obj.Send<float>("PHYSICS_SET_HORIZ", 0);
                if (onGround)
                {
                    obj.Send("GRAPHICS_PLAYANIMATION", "Idle");
                }
            }
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
                                gravity = (float)(object)obj;
                            break;                       

                        case "ONGROUND":
                            if (obj is bool)
                                onGround = (bool)(object)obj;
                            break;

                        case "JUMPCOUNT":
                            if (obj is int)
                                jumpCount = (int)(object)obj;
                            break;                        
                    }
                }
            }
        }
    }
}
