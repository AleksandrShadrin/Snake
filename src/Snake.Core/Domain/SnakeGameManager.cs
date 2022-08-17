using Snake.Core.Exceptions;
using Snake.Core.ValueObjects;

namespace Snake.Core.Domain
{
    public class SnakeGameManager
    {
        public uint Score { get; private set; }
        public List<RewardObject> RewardObjects { get; private set; } = new();

        private Predicate<SnakeGameManager> Conditions;
        private SnakeGameObject _snake;
        public SnakeGameManager(SnakeGameObject snake)
        {
            if (snake is null)
            {
                throw new SnakeIsEmptyException();
            }
            _snake = snake;
        }

        public void MoveSnake(PosXY toPos)
        {
            var snakeCollidedWithItself = _snake.CheckCollisionAtPosition(toPos);

            if (snakeCollidedWithItself)
            {
                _snake.KillSnake();
                return;
            }

            var collidedObjects = RewardObjects.
                Where(i => _snake.CheckCollisionAtPosition(i.Position));

            foreach (var item in collidedObjects)
            {
                RemoveRewardObject(item);
                AddScore(item.Reward);
            }
        }
        public void RemoveRewardObject(RewardObject rewardObject)
            => RewardObjects.Remove(rewardObject);

        public void AddRewardObject(RewardObject rewardObject)
        {
            var existedObject = RewardObjects.SingleOrDefault(i => i.Position == rewardObject.Position);

            if (existedObject is { })
            {
                RemoveRewardObject(existedObject);
            }

            RewardObjects.Add(rewardObject);
        }

        public SnakeGameManager AddGameOverConditions(Predicate<SnakeGameManager> condition)
        {
            Conditions += condition;
            return this;
        }

        public bool GameIsOver()
        {
            var result = Conditions?.Invoke(this);
            if (result is { })
            {
                return _snake.SnakeIsDead() || result.Value;
            }
            return _snake.SnakeIsDead();
        }

        public void AddScore(uint points)
        {
            Score += points;
        }
    }
}
