using Snake.Core.Domain;

namespace Snake.Core.Factories
{
    public interface ISnakeGameStateFactory
    {
        SnakeGameState CreateSnakeGameState(SnakeGameObject @object);
    }
}
