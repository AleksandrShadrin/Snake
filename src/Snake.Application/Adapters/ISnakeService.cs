using Snake.Application.Models;

namespace Snake.Application.Adapters
{
    public interface ISnakeService
    {
        void KillSnake();
        SnakeBodyObject GetSnakeBody();
    }
}
