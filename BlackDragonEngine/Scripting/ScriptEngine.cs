using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlackDragonEngine.Scripting
{
    public delegate IEnumerator<int> Script(params float[] parameters);

    public sealed class ScriptEngine : GameComponent
    {
        private readonly List<ScriptState> _scripts = new List<ScriptState>();

        public ScriptEngine(Game game)
            : base(game)
        {
        }

        public void ExecuteScript(Script script)
        {
            var scriptState = new ScriptState(script);
            ExecuteScript(scriptState);
        }

        private void ExecuteScript(ScriptState scriptState)
        {
            scriptState.Execute();
            if (!scriptState.IsComplete)
            {
                _scripts.Add(scriptState);
            }
        }

        public void ExecuteScript(Script script, params float[] parameters)
        {
            var scriptState = new ScriptState(script, parameters);
            ExecuteScript(scriptState);
        }

        public bool IsScriptRunning(Script script)
        {
            return _scripts.Exists(s => s.Script == script);
        }

        public void StopAllScripts()
        {
            _scripts.Clear();
        }

        public override void Update(GameTime gameTime)
        {
            if (EngineStates.GameStates == EEngineStates.Running)
            {
                for (int i = 0; i < _scripts.Count; ++i)
                {
                    _scripts[i].Execute();
                }
                _scripts.RemoveAll(s => s.IsComplete);
            }
        }

        #region Nested type: ScriptState

        private class ScriptState
        {
            private readonly float[] scriptParameter;
            private IEnumerator<int> scriptEnumerator;
            private int sleepLength;

            public ScriptState(Script script)
            {
                Script = script;
                scriptParameter = null;
            }

            public ScriptState(Script script, params float[] parameters)
                : this(script)
            {
                scriptParameter = parameters;
            }

            public Script Script { get; private set; }

            public bool IsComplete
            {
                get { return Script == null; }
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

        #endregion
    }
}