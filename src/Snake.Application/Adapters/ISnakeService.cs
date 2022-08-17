using Snake.Application.Models;
using Snake.Core.ValueObjects;

namespace Snake.Application.Adapters
{
    public interface ISnakeService
    {
        void MoveSnake(PosXY toPos);
        void KillSnake();
        void IncreaseSnake();
        bool CollidedAtPos(PosXY pos);
        void ChangeDirection(Direction direction);
        Direction GetDirection();
        SnakeBodyObject GetSnakeBody();
    }
}
