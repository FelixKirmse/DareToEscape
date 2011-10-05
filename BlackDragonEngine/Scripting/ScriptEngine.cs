using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BlackDragonEngine.Scripting
{
    public delegate IEnumerator<float> Script();

    public class ScriptEngine : GameComponent
    {
        private static readonly Dictionary<Script, ScriptState> scripts = new Dictionary<Script, ScriptState>();

        public ScriptEngine(Game game)
            : base(game)
        {         
        }

        public void ExecuteSript(Script script)
        {
            ScriptState scriptState = new ScriptState(script);
            scriptState.Execute(null);

            if (!scriptState.IsComplete)
            {
                scripts.Add(script, scriptState);
            }
        }

        public bool IsScriptRunning(Script script)
        {
            return scripts.ContainsKey(script);
        }

        public void StopAllScripts()
        {
            scripts.Clear();
        }

        public override void Update(GameTime gameTime)
        {
            List<Script> scriptsToRemove = new List<Script>();
            foreach (var scriptState in scripts)
            {
                scriptState.Value.Execute(gameTime);
                if (scriptState.Value.IsComplete)
                {
                    scriptsToRemove.Add(scriptState.Value.OrgScript);
                }
            }

            foreach (var script in scriptsToRemove)
            {
                scripts.Remove(script);                
            }            
        }

        private class ScriptState
        {
            private float sleepLength;
            public Script Script { get; private set; }
            public Script OrgScript { get; private set; }
            private IEnumerator<float> scriptEnumerator;

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
                OrgScript = script;
            }

            public void Execute(GameTime gameTime)
            {
                if (scriptEnumerator == null)
                {
                    scriptEnumerator = Script();
                    sleepLength = scriptEnumerator.Current;
                }

                if (sleepLength > 0 && gameTime != null)
                {
                    sleepLength -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
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
