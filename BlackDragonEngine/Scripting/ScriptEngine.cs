using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BlackDragonEngine.Scripting
{
    public delegate IEnumerator<int> Script(params float[] parameters);     

    public class ScriptEngine : GameComponent
    {
        private static readonly List<ScriptState> scripts = new List<ScriptState>();        

        public ScriptEngine(Game game)
            : base(game)
        {           
        }

        public void ExecuteScript(Script script)
        {
            ScriptState scriptState = new ScriptState(script);
            ExecuteScript(scriptState);
        }

        private void ExecuteScript(ScriptState scriptState)
        {
            scriptState.Execute();
            if (!scriptState.IsComplete)
            {
                scripts.Add(scriptState);
            }
        }

        public void ExecuteScript(Script script, params float[] parameters)
        {
            ScriptState scriptState = new ScriptState(script, parameters);
            ExecuteScript(scriptState);
        }        

        public bool IsScriptRunning(Script script)
        {
            return scripts.Exists(s => s.Script == script);
        }

        public void StopAllScripts()
        {
            scripts.Clear();
        }

        public override void Update(GameTime gameTime)
        {
            if (EngineStates.GameStates == EEngineStates.Running)
            {
                for (int i = 0; i < scripts.Count; ++i)
                {
                    scripts[i].Execute();
                }
                scripts.RemoveAll(s => s.IsComplete);
            }            
        }        

        private class ScriptState
        {
            private int sleepLength;
            public Script Script { get; private set; }            
            private IEnumerator<int> scriptEnumerator;
            private float[] scriptParameter;

            public bool IsComplete
            {
                get
                {
                    return Script == null;
                }
            }

            public ScriptState(Script script)
            {
                this.Script = script;                
                scriptParameter = null;
            }

            public ScriptState(Script script, params float[] parameters)
                : this(script)
            {
                scriptParameter = parameters;
            }

            public void Execute()
            {
                if (scriptEnumerator == null)
                {
                    scriptEnumerator = Script(scriptParameter);
                    sleepLength = scriptEnumerator.Current;
                }

                if (sleepLength > 0)
                {
                    --sleepLength;
                }
                else
                {
                    bool unfinished = false;
                    do
                    {
                        unfinished = scriptEnumerator.MoveNext();
                        sleepLength = scriptEnumerator.Current;
                    } while (sleepLength <= 0 && unfinished);

                    if (!unfinished)
                    {
                        Script = null;
                        scriptEnumerator = null;
                    }
                }
            }
        }
    }
}
