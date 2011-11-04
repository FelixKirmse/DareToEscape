using System.Collections.Generic;
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

        public static void OnMapCodeCheck(string[] code, Vector2 location, GameObject player)
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

        public static void OnCodeUnderPlayerCheck(string[] codeArray, GameObject player)
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

        public static int OnCodeInPlayerCenterCheck(string[] codeArray, List<string> codes, Vector2 collisionCenter,
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


        public static void Spawn(string[] codearray, Vector2 position)
        {
            switch (codearray[1])
            {
                case "TURRET":
                    switch (codearray[2])
                    {
                        case "SMALL":
                            GameObject smallTurret = Factory.CreateSmallTurret();
                            smallTurret.Position = position;
                            smallTurret.Send("SET_" + codearray[3], smallTurret);
                            EntityManager.AddEntity(smallTurret);
                            break;

                        case "MEDIUM":
                            GameObject mediumTurret = Factory.CreateMediumTurret();
                            mediumTurret.Position = position;
                            mediumTurret.Send("SET_" + codearray[3], mediumTurret);
                            EntityManager.AddEntity(mediumTurret);
                            break;

                        case "BOSS1":
                            GameObject boss1 = Factory.CreateBoss1();
                            boss1.Position = position;
                            boss1.Send("SET_" + codearray[3], boss1);
                            EntityManager.AddEntity(boss1);
                            GameVariableProvider.Bosses.Add(boss1);
                            break;

                        case "TUTORIALBOSS":
                            GameObject tutorialBoss = Factory.CreateTutorialBoss();
                            tutorialBoss.Position = position;
                            tutorialBoss.Send("SET_" + codearray[3], tutorialBoss);
                            EntityManager.AddEntity(tutorialBoss);
                            GameVariableProvider.Bosses.Add(tutorialBoss);
                            break;
                    }
                    break;
            }
        }
    }
}