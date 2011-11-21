using System;
using System.Collections.Generic;
using BlackDragonEngine.Components;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Managers;
using DareToEscape.Helpers;
using DareToEscape.Providers;
using Microsoft.Xna.Framework;

namespace DareToEscape.Managers
{
    internal static class CodeHandler
    {
        public static void BindEvents()
        {
            CodeManager.OnMapCodeCheck += OnMapCodeCheck;
            CodeManager.OnCodeUnderPlayerCheck += OnCodeUnderPlayerCheck;
            CodeManager.OnCodeInPlayerCenterCheck += OnCodeInPlayerCenterCheck;
        }

        private static void OnMapCodeCheck(string[] code, Vector2 location, GameObject player)
        {
            switch (code[0])
            {
                case "START":
                    player.Position = location;
                    break;

                case "SPAWN":
                    Spawn(code, location);
                    break;

                case "CHECKPOINT":
                    GameObject checkPoint = Factory.CreateCheckPoint();
                    checkPoint.Position = location;
                    EntityManager.AddEntity(checkPoint);
                    break;

                case "EXIT":
                    GameObject exit = Factory.CreateExit();
                    exit.Position = location;
                    EntityManager.AddEntity(exit);
                    break;

                case "KEY":
                    GameObject key = Factory.CreateKey();
                    key.Position = location;
                    EntityManager.AddEntity(key);
                    key.Send("KEYSTRING", code[1]);
                    break;

                case "LOCK":
                    GameObject Lock = Factory.CreateLock();
                    Lock.Position = location;
                    EntityManager.AddEntity(Lock);
                    Lock.Send("KEYSTRING", code[1]);
                    break;

                case "BOSSKILLER":
                    GameObject bossKiller = Factory.CreateBossKiller();
                    bossKiller.Position = location;
                    EntityManager.AddEntity(bossKiller);
                    break;

                case "DIALOG":
                    GameObject sign = Factory.CreateSign();
                    sign.Position = location;
                    EntityManager.AddEntity(sign);
                    break;
            }
        }

        private static void OnCodeUnderPlayerCheck(string[] codeArray, GameObject player)
        {
            switch (codeArray[0])
            {
                case "JUMPTHROUGHTOP":
                    player.Send("PHYSICS_SET_JUMPTHROUGHCHECK", true);
                    break;

                case "WATER":
                    player.Send("PHYSICS_SET_INWATER", true);
                    break;
            }
        }

        private static int OnCodeInPlayerCenterCheck(IList<string> codeArray, List<string> codes,
                                                     Vector2 collisionCenter,
                                                     int i, GameObject player)
        {
            switch (codeArray[0])
            {
                case "TRANSITION":
                    IngameManager.Activate();
                    LevelManager.LoadLevel(codeArray[1]);
                    SaveManager<SaveState>.CurrentSaveState.Keys.Clear();
                    SaveManager<SaveState>.CurrentSaveState.BossDead = false;
                    break;

                case "MAINMENU":
                    StateManager.GameState = GameStates.Menu;
                    StateManager.MenuState = MenuStates.Main;
                    SaveManager<SaveState>.CurrentSaveState.Keys.Clear();
                    SaveManager<SaveState>.CurrentSaveState.BossDead = false;
                    break;

                case "DIALOG":
                    if (InputMapper.StrictAction)
                    {
                        DialogHelper.PlayDialog(codeArray[1]);
                    }
                    break;

                case "TUTORIALDIALOG":
                    DialogManager.PlayDialog(DialogDictionaryProvider.TutorialDialog(), "Tutorial");
                    codes.Remove("TUTORIALDIALOG");
                    break;

                case "TUTORIALFINISH":
                    DialogManager.PlayDialog(DialogDictionaryProvider.TutorialDialogFinish(), "TutorialFinish");
                    codes.Remove("TUTORIALFINISH");
                    codes.Add("MAINMENU");
                    break;

                case "GRATZ":
                    DialogManager.PlayDialog(DialogDictionaryProvider.Gratz(), "Gratz");
                    codes.Remove("GRATZ");
                    codes.Add("MAINMENU");
                    break;

                case "SAVE":
                    SaveManager<SaveState>.Save();
                    codes.Remove("SAVE");
                    --i;
                    break;

                case "WALKLEFT":
                    player.Send("PHYSICS_SET_NORIGHT", true);
                    break;

                case "WALKRIGHT":
                    player.Send("PHYSICS_SET_NOLEFT", true);
                    break;

                case "DEADLY":
                    player.Send<string>("KILL", null);
                    break;

                case "TRIGGER":
                    if (codeArray[1] == "BOSS")
                        foreach (GameObject boss in GameVariableProvider.Bosses)
                            boss.Send<string>("SHOOT", null);
                    break;
            }
            return i;
        }


        private static void Spawn(IList<string> codearray, Vector2 position)
        {
            var components = new List<IComponent>
                                 {
                                     (IComponent)
                                     Activator.CreateInstance(
                                         Type.GetType("DareToEscape.Components.Entities." + codearray[1] + "Component"))
                                 };
            var turret = new GameObject(components) {Position = position};
            turret.Send("SET_" + codearray[2], turret);
            if (codearray[1].Contains("Boss"))
            {
                GameVariableProvider.Bosses.Add(turret);
            }
            EntityManager.AddEntity(turret);
        }
    }
}