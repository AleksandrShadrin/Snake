using Snake.Core.Constants;
using Snake.Core.Exceptions;
using Snake.Core.ValueObjects;

namespace Snake.Core.Domain
{
    public class SnakeGameManager
    {
        public uint Score { get; private set; }
        public Direction MoveDirection { get; private set; } = Direction.RIGHT;

        private Predicate<SnakeGameManager> conditions;
        private SnakeGameObject _snake;
        private List<RewardObject> RewardObjects = new();

        public SnakeGameManager(SnakeGameObject snake)
        {
            if (snake is null)
            {
                throw new SnakeIsEmptyException();
            }
            _snake = snake;
        }

        public void ChangeDirection(Direction direction)
        {
            if ((int)direction == -(int)MoveDirection)
            {
                throw new WrongDirectionException();
            }

            MoveDirection = direction;
        }

        public IReadOnlyList<RewardObject> GetRewardObjects()
            => RewardObjects;
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
            conditions += condition;
            return this;
        }

        public bool GameIsOver()
        {
            var result = conditions?.Invoke(this);
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

        public void MoveSnake()
        {
            var head = _snake.GetHead();

            var newPos = MoveDirection switch
            {
                Direction.TOP => head with { X = head.X, Y = head.Y + 1 },
                Direction.BOTTOM => head with { X = head.X, Y = head.Y - 1 },
                Direction.RIGHT => head with { X = head.X + 1, Y = head.Y },
                Direction.LEFT => head with { X = head.X - 1, Y = head.Y },
            };

            MoveSnake(newPos);
        }

        private void MoveSnake(PosXY toPos)
        {
            var snakeCollidedWithItself = _snake.CheckCollisionAtPosition(toPos);

            if (snakeCollidedWithItself)
            {
                _snake.KillSnake();
                return;
            }

            _snake.Move(toPos);

            CheckCollisionWithRewards();
        }

        private void CheckCollisionWithRewards()
        {
            var collidedObjects = RewardObjects.
                            Where(i => _snake.CheckCollisionAtPosition(i.Position))
                            .ToList();

            foreach (var item in collidedObjects)
            {
                RemoveRewardObject(item);
                AddScore(item.Reward);
                _snake.IncreaseSnake();
            }
        }
    }
}
