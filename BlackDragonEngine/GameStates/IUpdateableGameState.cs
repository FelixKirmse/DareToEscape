namespace BlackDragonEngine.GameStates
{
    internal interface IUpdateableGameState
    {
        bool UpdateCondition { get; }
        void Update();
    }
}