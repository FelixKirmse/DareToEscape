﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Providers;
using DareToEscape.Managers;
using BlackDragonEngine.Managers;
using Microsoft.Xna.Framework;

namespace DareToEscape.Helpers
{
     static class GameInitializer
    {
         public static void Initialize()
         {
             InputMapper.ActionKeys = new Keys[] { Keys.Enter, Keys.E };
             InputMapper.JumpKeys = new Keys[] { Keys.Space, Keys.Space };
             InputMapper.UpKeys = new Keys[] { Keys.W, Keys.Up };
             InputMapper.DownKeys = new Keys[] { Keys.S, Keys.Down };
             InputMapper.LeftKeys = new Keys[] { Keys.A, Keys.Left };
             InputMapper.RightKeys = new Keys[] { Keys.D, Keys.Right };
             InputMapper.CancelKeys = new Keys[] { Keys.Escape };

             VariableProvider.GenerateNewRandomSeed();
             VariableProvider.SaveSlot = "1";
             SaveManager<SaveState>.CurrentSaveState = new SaveState();

             LevelManager.OnLevelLoad += ((DareToEscape)VariableProvider.Game).OnLevelLoad;
             SaveManager<SaveState>.SaveHelper.OnSave += SaveHelper.OnSave;
             SaveManager<SaveState>.SaveHelper.OnLoad += SaveHelper.OnLoad;

             Camera.ViewPortHeight = 600;
             Camera.ViewPortWidth = 800;

             #region Sound init
             Dictionary<string, string> parameters = new Dictionary<string, string>();
             parameters.Add("settingsFile", @"Content/audio/DareToEscapeAudio.xgs");
             parameters.Add("bgmBank", @"Content/audio/Music.xwb");
             parameters.Add("sfxBank", @"Content/audio/Music.xwb");
             parameters.Add("soundBank", @"Content/audio/Music.xsb");
             parameters.Add("sfxCategory", "Default");
             parameters.Add("bgmCategory", "Default");
             AudioManager.Initialize(parameters);
             #endregion

             DialogManager.DrawMugshot = false;

             StateManager.Initialize();
         }         
    }
}
