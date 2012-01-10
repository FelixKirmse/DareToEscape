using System;
using System.Collections.Generic;
using BlackDragonEngine.GameStates;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Managers;
using BlackDragonEngine.Providers;
using DareToEscape.Bullets.Behaviors;
using DareToEscape.Managers;
using Microsoft.Xna.Framework.Input;

namespace DareToEscape.Helpers
{
    public static class GameInitializer
    {
        public static void Initialize()
        {
            InputMapper.ActionKeys = new[] {Keys.Enter, Keys.E};
            InputMapper.JumpKeys = new[] {Keys.Space, Keys.Space};
            InputMapper.UpKeys = new[] {Keys.W, Keys.Up};
            InputMapper.DownKeys = new[] {Keys.S, Keys.Down};
            InputMapper.LeftKeys = new[] {Keys.A, Keys.Left};
            InputMapper.RightKeys = new[] {Keys.D, Keys.Right};
            InputMapper.CancelKeys = new[] {Keys.Escape};
            var focusKeys = new[] {Keys.LeftShift, Keys.RightShift};
            InputMapper.AddNewAction("Focus", new List<Keys>(focusKeys));

            VariableProvider.GenerateNewRandomSeed();
            VariableProvider.SaveSlot = "1";
            SaveManager<SaveState>.CurrentSaveState = new SaveState();

            SaveManager<SaveState>.SaveHelper.OnSave += SaveHelper.OnSave;
            SaveManager<SaveState>.SaveHelper.OnLoad += SaveHelper.OnLoad;

            DialogManager.DrawMugshot = false;

            CodeHandler.BindEvents();
            ReusableBehaviors.Initialize();

            VariableProvider.ProcessorCount = Environment.ProcessorCount;
        }
    }
}