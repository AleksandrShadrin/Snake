using Snake.Application.Models;
using Snake.Core.Constants;
using Snake.Core.Domain;
using Snake.Core.Exceptions;
using Snake.Core.Factories;
using Snake.Core.ValueObjects;

namespace Snake.Application.Adapters
{
    public class SnakeGameService : ISnakeGameService
    {
        private readonly ISnakeGameManagerFactory _gameManagerFactory;
        private readonly ISnakeGameObjectFactory _gameObjectFactory;
        private SnakeGameObject _snakeGameObject;
        private SnakeGameManager _snakeGameManager;

        public SnakeGameService(ISnakeGameManagerFactory gameManagerFactory, ISnakeGameObjectFactory gameObjectFactory)
        {
            _gameManagerFactory = gameManagerFactory is null
                ? throw new ArgumentNullException(nameof(gameManagerFactory))
                : gameManagerFactory;
            _gameObjectFactory = gameObjectFactory is null
                ? throw new ArgumentNullException(nameof(gameObjectFactory))
                : gameObjectFactory;
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

        public Direction CurrentDirection()
        {
            return _snakeGameManager.MoveDirection;
        }

        public bool GameIsOver()
        {
            return _snakeGameManager.GameIsOver();
        }

        public Level GetLevel()
        {
            return _snakeGameManager.Level;
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

        public void LoadGame(SnakeGameData data)
        {
            var allBody = new LinkedList<PosXY>();

            if (data.Snake.Tail is { })
            {
                allBody.AddFirst(data.Snake.Tail);
            }

            data.Snake.Body.ToList().ForEach(i => allBody.AddLast(i));

            allBody.AddLast(data.Snake.Head);

            _snakeGameObject = new(allBody);
            _snakeGameManager = _gameManagerFactory.CreateSnakeGameManager(_snakeGameObject, data.Level);

            try
            {
                _snakeGameManager.ChangeDirection(data.Direction);
            }
            catch (SnakeException ex)
            {
            }


            foreach (var reward in data.RewardObjects)
            {
                this.AddRewardObject(reward);
            }

            _snakeGameManager.AddScore(data.Score);
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
