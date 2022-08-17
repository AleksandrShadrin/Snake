using Snake.Core.Domain;

namespace Snake.Core.Factories
{
    public interface ISnakeGameStateFactory
    {
        SnakeGameManager CreateSnakeGameManager(SnakeGameObject @object);
    }
}
