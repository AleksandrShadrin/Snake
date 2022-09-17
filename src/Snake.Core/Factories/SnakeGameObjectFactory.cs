using Snake.Core.Domain;
using Snake.Core.ValueObjects;

namespace Snake.Core.Factories
{
    public class SnakeGameObjectFactory : ISnakeGameObjectFactory
    {
        public SnakeGameObject CreateSnakeGameObject(PosXY pos)
        {
            return new SnakeGameObject(pos);
        }
    }
}
