using Snake.Application.Models;
using Snake.Core.Constants;
using Snake.Core.ValueObjects;

namespace Snake.Application.Adapters
{
    public interface ISnakeGameService
    {
        bool GameIsOver();
        uint GetScore();
        void AddScore(uint points);
        void AddRewardObject(RewardObject reward);
        void RemoveRewardObject(RewardObject reward);
        void MoveSnake();
        void ChangeMoveDirection(Direction direction);
        SnakeBodyObject GetSnakeBody();
    }
}
