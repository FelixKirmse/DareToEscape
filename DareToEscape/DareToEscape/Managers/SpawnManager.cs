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
using DareToEscape.Providers;

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
                            GameObject smallTurret = Factory.CreateSmallTurret();
                            smallTurret.Position = position;
                            smallTurret.Send<GameObject>("SET_" + codearray[3], smallTurret);
                            EntityManager.AddEntity(smallTurret);
                            break;

                        case "MEDIUM":
                            GameObject mediumTurret = Factory.CreateMediumTurret();
                            mediumTurret.Position = position;
                            mediumTurret.Send<GameObject>("SET_" + codearray[3], mediumTurret);
                            EntityManager.AddEntity(mediumTurret);
                            break;

                        case "BOSS1":
                            GameObject boss1 = Factory.CreateBoss1();
                            boss1.Position = position;
                            boss1.Send<GameObject>("SET_" + codearray[3], boss1);
                            EntityManager.AddEntity(boss1);
                            GameVariableProvider.Boss = boss1;
                            break;
                    }
                    break;
            }
        }
    }
}
