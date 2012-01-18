using System;
using System.Collections.Generic;
using BlackDragonEngine;
using BlackDragonEngine.Components;
using BlackDragonEngine.Entities;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Managers;
using BlackDragonEngine.TileEngine;
using DareToEscape.GameStates;
using DareToEscape.Helpers;
using DareToEscape.Providers;
using Microsoft.Xna.Framework;

namespace DareToEscape.Managers
{
    internal static class CodeHandler
    {
        public static void BindEvents()
        {
            CodeManager<TileCode>.OnMapCodeCheck += OnMapCodeCheck;
            CodeManager<TileCode>.OnCodeUnderPlayerCheck += OnCodeUnderPlayerCheck;
            CodeManager<TileCode>.OnCodeInPlayerCenterCheck += OnCodeInPlayerCenterCheck;
        }

        private static void OnMapCodeCheck(TileCode code, Vector2 location, GameObject player)
        {
            switch (code.Code)
            {
                case TileCodes.Start:
                    player.Position = location;
                    break;

                case TileCodes.Spawn:
                    Spawn(code, location);
                    break;

                case TileCodes.Checkpoint:
                    GameObject checkPoint = Factory.CreateCheckPoint();
                    checkPoint.Position = location;
                    EntityManager.AddEntity(checkPoint);
                    break;

                case TileCodes.Exit:
                    GameObject exit = Factory.CreateExit();
                    exit.Position = location;
                    EntityManager.AddEntity(exit);
                    break;

                case TileCodes.Key:
                    GameObject key = Factory.CreateKey();
                    key.Position = location;
                    EntityManager.AddEntity(key);
                    key.Send("KEYSTRING", code.Message);
                    break;

                case TileCodes.Lock:
                    GameObject Lock = Factory.CreateLock();
                    Lock.Position = location;
                    EntityManager.AddEntity(Lock);
                    Lock.Send("KEYSTRING", code.Message);
                    break;

                case TileCodes.Dialog:
                    GameObject sign = Factory.CreateSign();
                    sign.Position = location;
                    EntityManager.AddEntity(sign);
                    break;
            }
        }

        private static void OnCodeUnderPlayerCheck(TileCode code, GameObject player)
        {
            switch (code.Code)
            {
                case TileCodes.JumpthroughTop:
                    player.Send("PHYSICS_SET_JUMPTHROUGHCHECK", true);
                    break;

                case TileCodes.Water:
                    player.Send("PHYSICS_SET_INWATER", true);
                    break;
            }
        }

        private static int OnCodeInPlayerCenterCheck(TileCode code, List<TileCode> codes,
                                                     Vector2 collisionCenter,
                                                     int i, GameObject player)
        {
            switch (code.Code)
            {
                case TileCodes.Transition:
                    if (EngineState.GameState == EngineStates.Editor) break;
                    Ingame.GetInstance().Activate();
                    LevelManager.LoadLevel<Map<TileCode>, TileCode>(code.Message);
                    GameVariableProvider.SaveManager.CurrentSaveState.Keys.Clear();
                    GameVariableProvider.SaveManager.CurrentSaveState.BossDead = false;
                    break;

                case TileCodes.Dialog:
                    if (InputMapper.StrictAction)
                    {
                        DialogHelper.PlayDialog(code.Message);
                    }
                    break;

                case TileCodes.Save:
                    GameVariableProvider.SaveManager.Save();
                    codes.Remove(new TileCode(TileCodes.Save));
                    --i;
                    break;

                case TileCodes.PushLeft:
                    player.Send("PHYSICS_PUSHLEFT", true);
                    break;

                case TileCodes.PushRight:
                    player.Send("PHYSICS_PUSHRIGHT", true);
                    break;

                case TileCodes.PushUp:
                    player.Send("PHYSICS_PUSHUP", true);
                    break;

                case TileCodes.PushDown:
                    player.Send("PHYSICS_PUSHDOWN", true);
                    break;

                case TileCodes.Deadly:
                    player.Send<string>("KILL", null);
                    break;

                case TileCodes.Trigger:
                    if (code.Message == "BOSS")
                        foreach (var boss in GameVariableProvider.Bosses)
                            boss.Send<string>("SHOOT", null);
                    break;
            }
            return i;
        }


        private static void Spawn(TileCode code, Vector2 position)
        {
            string[] codearray = code.Message.Split('_');
            var components = new List<IComponent>
                                 {
                                     (IComponent)
                                     Activator.CreateInstance(
                                         Type.GetType("DareToEscape.Components.Entities." + codearray[0] + "Component"))
                                 };
            var turret = new GameObject(components) {Position = position};
            turret.Send("SET_" + codearray[1], turret);
            if (codearray[0].Contains("Boss"))
            {
                GameVariableProvider.Bosses.Add(turret);
            }
            EntityManager.AddEntity(turret);
        }
    }
}