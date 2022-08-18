using Snake.Core.ValueObjects;

namespace Snake.Application.Adapters
{
    public interface IGameManagerService
    {
        bool GameIsOver();
        uint GetScore();
        void AddScore(uint points);
        void AddRewardObject(RewardObject reward);
        void RemoveRewardObject(RewardObject reward);
        void MoveSnake();
        void ChangeMoveDirection(Direction direction);
    }
}
