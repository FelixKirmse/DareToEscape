namespace DareToEscape
{
    public struct TileCode
    {
        public TileCodes Code;
        public string Message;

        public TileCode(TileCodes code, string message = null)
        {
            Code = code;
            Message = message;
        }
    }

    public enum TileCodes
    {
        Start,
        Transition,
        Spawn,
        Checkpoint,
        Exit,
        Key,
        Lock,
        Dialog,
        Jumpthrough,
        JumpthroughTop,
        Water,
        MainMenu,
        Save,
        WalkLeft,
        WalkRight,
        Deadly,
        Trigger
    }
}