using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackDragonEngine
{   
    public enum DialogueStates { Talking, Pause , Active, Inactive }

    public static class EngineStates
    {
        public static DialogueStates DialogState { get; set; }
    }
}
