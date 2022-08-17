using Snake.Core.Domain;
using Snake.Core.ValueObjects;

namespace Snake.Core.Factories
{
    public interface ISnakeGameObjectFactory
    {
        SnakeGameObject CreateSnakeGameObject(PosXY pos);
    }
}
