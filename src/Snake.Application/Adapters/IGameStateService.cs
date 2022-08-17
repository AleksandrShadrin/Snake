namespace Snake.Application.Adapters
{
    public interface IGameStateService
    {
        bool GameIsOver();
        uint GetScore();
        void AddScore(uint points);
    }
}
