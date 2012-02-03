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
            if (EngineState.GameState == EngineStates.Running || EngineState.GameState == EngineStates.Editor)
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
            private readonly float[] _scriptParameter;
            private IEnumerator<int> _scriptEnumerator;
            private int _sleepLength;

            public ScriptState(Script script)
            {
                Script = script;
                _scriptParameter = null;
            }

            public ScriptState(Script script, params float[] parameters)
                : this(script)
            {
                _scriptParameter = parameters;
            }

            public Script Script { get; private set; }

            public bool IsComplete
            {
                get { return Script == null; }
            }

            public void Execute()
            {
                if (_scriptEnumerator == null)
                {
                    _scriptEnumerator = Script(_scriptParameter);
                    _sleepLength = _scriptEnumerator.Current;
                }

                if (_sleepLength > 0)
                {
                    --_sleepLength;
                }
                else
                {
                    bool unfinished;
                    do
                    {
                        unfinished = _scriptEnumerator.MoveNext();
                        _sleepLength = _scriptEnumerator.Current;
                    } while (_sleepLength <= 0 && unfinished);

                    if (!unfinished)
                    {
                        Script = null;
                        _scriptEnumerator = null;
                    }
                }
            }
        }

        #endregion
    }
}