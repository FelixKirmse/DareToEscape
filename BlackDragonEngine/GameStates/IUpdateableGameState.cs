namespace BlackDragonEngine.GameStates
{
    public interface IUpdateableGameState
    {
        bool UpdateCondition { get; }
        bool Update();
    }
}