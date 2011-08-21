using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DareToEscape.Entities;
using DareToEscape.Components;
using DareToEscape.Managers;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Components;
using DareToEscape.Helpers;
using BlackDragonEngine.Managers;

namespace DareToEscape.Managers
{
    static class SpawnManager
    {
        public static void Spawn(string[] codearray, Vector2 position)
        {
            switch (codearray[1])
            { 
                case "TURRET":
                    switch(codearray[2])
                    {
                        case "SMALL":
                            GameObject turret = Factory.CreateSmallTurret();
                            turret.Position = position;
                            turret.Send<GameObject>("SET_" + codearray[3], turret);
                            EntityManager.AddEntity(turret);
                            break;
                    }
                    break;
            }
        }
    }
}
