namespace BlackDragonEngine
{
    public enum DialogueStates
    {
        Talking,
        Pause,
        BreakPause,
        Active,
        Inactive
    }

    public static class EngineState
    {
        public static DialogueStates DialogState { get; set; }
        public static EngineStates GameState { get; set; }
    }
}