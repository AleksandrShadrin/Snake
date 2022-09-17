using Snake.Application.Models;
using Snake.Core.Constants;
using Snake.Core.Domain;
using Snake.Core.Factories;
using Snake.Core.ValueObjects;

namespace Snake.Application.Adapters
{
    public class SnakeGameService : ISnakeGameService
    {
        private ISnakeGameManagerFactory _gameManagerFactory;
        private ISnakeGameObjectFactory _gameObjectFactory;
        private SnakeGameObject _snakeGameObject;
        private SnakeGameManager _snakeGameManager;

        public SnakeGameService(ISnakeGameManagerFactory gameManagerFactory, ISnakeGameObjectFactory gameObjectFactory)
        {
            _gameManagerFactory = gameManagerFactory is null ? throw new ArgumentNullException(nameof(gameManagerFactory)) : gameManagerFactory;
            _gameObjectFactory = gameObjectFactory is null ? throw new ArgumentNullException(nameof(gameObjectFactory)) : gameObjectFactory;
        }

        public void AddRewardObject(RewardObject reward)
        {
            _snakeGameManager?.AddRewardObject(reward);
        }

        public void ChangeMoveDirection(Direction direction)
        {
            _snakeGameManager?.ChangeDirection(direction);
        }

        public void CreateGame(PosXY pos, Level level)
        {
            _snakeGameObject = _gameObjectFactory.CreateSnakeGameObject(pos);
            _snakeGameManager = _gameManagerFactory.CreateSnakeGameManager(_snakeGameObject, level);
        }

        public bool GameIsOver()
        {
            return _snakeGameManager.GameIsOver();
        }

        public IReadOnlyList<RewardObject> GetRewards()
        {
            return _snakeGameManager.GetRewardObjects();
        }

        public uint GetScore()
        {
            return _snakeGameManager.Score;
        }

        public SnakeBodyObject GetSnakeBody()
        {
            return new(_snakeGameObject.GetHead(), _snakeGameObject.GetTail(), _snakeGameObject.GetBody());
        }

        public IEnumerable<PosXY> GetWalls()
        {
            return _snakeGameManager.Level.Walls;
        }

        public void MoveSnake()
        {
            _snakeGameManager?.MoveSnake();
        }

        public void RemoveRewardObject(RewardObject reward)
        {
            _snakeGameManager?.RemoveRewardObject(reward);
        }
    }
}
