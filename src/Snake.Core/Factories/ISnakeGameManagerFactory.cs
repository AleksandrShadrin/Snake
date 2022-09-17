using Snake.Core.Domain;
using Snake.Core.ValueObjects;

namespace Snake.Core.Factories
{
    public interface ISnakeGameManagerFactory
    {
        SnakeGameManager CreateSnakeGameManager(SnakeGameObject @object, Level level);
    }
}
