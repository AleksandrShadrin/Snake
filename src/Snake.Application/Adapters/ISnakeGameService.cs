using Snake.Application.Models;
using Snake.Core.Constants;
using Snake.Core.ValueObjects;

namespace Snake.Application.Adapters
{
    public interface ISnakeGameService
    {
        void CreateGame(PosXY pos, Level level);
        bool GameIsOver();
        uint GetScore();
        void AddRewardObject(RewardObject reward);
        void RemoveRewardObject(RewardObject reward);
        void MoveSnake();
        void ChangeMoveDirection(Direction direction);
        IEnumerable<PosXY> GetWalls();
        IReadOnlyList<RewardObject> GetRewards();
        SnakeBodyObject GetSnakeBody();
    }
}
