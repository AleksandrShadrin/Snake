using Snake.Core.Domain;
using Snake.Core.ValueObjects;

namespace Snake.Core.Factories
{
    public class SnakeGameManagerFactory : ISnakeGameManagerFactory
    {
        public SnakeGameManager CreateSnakeGameManager(SnakeGameObject @object, Level level)
        {
            return new SnakeGameManager(@object, level);
        }
    }
}
