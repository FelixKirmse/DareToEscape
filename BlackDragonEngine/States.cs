using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackDragonEngine
{   
    public enum DialogueStates { Talking, Pause, BreakPause, Active, Inactive }
    public enum EEngineStates { Paused, Running, Loading }

    public static class EngineStates
    {
        public static DialogueStates DialogState { get; set; }
        public static EEngineStates GameStates { get; set; }
    }
}
