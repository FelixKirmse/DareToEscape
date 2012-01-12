namespace BlackDragonEngine.GameStates
{
    public interface IDrawableGameState
    {
        bool DrawCondition { get; }
        void Draw();
    }
}