using Microsoft.Xna.Framework.Input;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Providers;
using DareToEscape.Managers;
using BlackDragonEngine.Managers;
using DareToEscape.Entities.BulletBehaviors;
using System.Collections.Generic;

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
             Keys[] focusKeys = { Keys.LeftShift, Keys.RightShift };
             InputMapper.AddNewAction("Focus", new List<Keys>(focusKeys));

             VariableProvider.GenerateNewRandomSeed();
             VariableProvider.SaveSlot = "1";
             SaveManager<SaveState>.CurrentSaveState = new SaveState();

             LevelManager.OnLevelLoad += ((DareToEscape)VariableProvider.Game).OnLevelLoad;
             SaveManager<SaveState>.SaveHelper.OnSave += SaveHelper.OnSave;
             SaveManager<SaveState>.SaveHelper.OnLoad += SaveHelper.OnLoad;

             Camera.ViewPortHeight = 720;
             Camera.ViewPortWidth = 1280;             

             DialogManager.DrawMugshot = false;
             
             StateManager.Initialize();
             CodeHandler.BindEvents();
             ReusableBehaviors.Initialize(); 
         }         
    }
}
