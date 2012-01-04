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

        public static bool operator ==(TileCode a, TileCode b)
        {
            return a.Code == b.Code;
        }

        public static bool operator !=(TileCode a, TileCode b)
        {
            return !(a == b);
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