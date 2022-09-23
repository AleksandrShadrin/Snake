using Snake.Core.Constants;
using Snake.Core.Exceptions;
using Snake.Core.ValueObjects;

namespace Snake.Core.Domain
{
    public class SnakeGameManager
    {
        public uint Score { get; private set; }
        public Direction MoveDirection { get; private set; } = Direction.RIGHT; // Змейка начинает движение всегда вправо
        public Level Level { get; private set; }

        private bool snakeCanChangeDirection = true;
        private Predicate<SnakeGameManager> conditions;
        private SnakeGameObject _snake;
        private List<RewardObject> RewardObjects = new();

        public SnakeGameManager(SnakeGameObject snake, Level level)
        {
            if (snake is null)
            {
                throw new SnakeIsEmptyException();
            }
            if (level is null)
            {
                throw new ArgumentNullException(nameof(level));
            }
            _snake = snake;
            Level = level;
        }

        public void ChangeDirection(Direction direction)
        {
            if(snakeCanChangeDirection is false)
                throw new TryChangeSnakeDirectionMoreThenOncePerMoveException();

            if (Math.Abs((int)direction) == Math.Abs((int)MoveDirection))
            {
                throw new WrongDirectionException();
            }

            snakeCanChangeDirection = false;
            MoveDirection = direction;
        }

        public IReadOnlyList<RewardObject> GetRewardObjects()
            => RewardObjects;
        public void RemoveRewardObject(RewardObject rewardObject)
            => RewardObjects.Remove(rewardObject);

        // Добавляет объект награды, если в его позиции уже есть данный объект,
        // то происходит замена на новый
        public void AddRewardObject(RewardObject rewardObject)
        {
            var existedObject = RewardObjects.SingleOrDefault(i => i.Position == rewardObject.Position);

            if (existedObject is { })
            {
                RemoveRewardObject(existedObject);
            }

            if (Level.Walls.Contains(rewardObject.Position))
                throw new RewardObjectCantBeInWallPosException();

            RewardObjects.Add(rewardObject);
        }
        // Добавление условий окончания игры
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

            PosXY newPos = GenerateNewPos(head);

            MoveSnake(newPos);
            snakeCanChangeDirection = true;
        }

        private PosXY GenerateNewPos(PosXY prevPos)
        {
            return MoveDirection switch
            {
                Direction.TOP => prevPos with { X = prevPos.X, Y = (prevPos.Y - 1) < 0 ? Level.GameSize.Y : prevPos.Y - 1 },
                Direction.BOTTOM => prevPos with { X = prevPos.X, Y = (prevPos.Y + 1) >= Level.GameSize.Y ? 0 : prevPos.Y + 1 },
                Direction.RIGHT => prevPos with { X = (prevPos.X + 1) >= Level.GameSize.X ? 0 : prevPos.X + 1, Y = prevPos.Y },
                Direction.LEFT => prevPos with { X = (prevPos.X - 1) < 0 ? Level.GameSize.X : prevPos.X - 1, Y = prevPos.Y },
            };
        }

        private void MoveSnake(PosXY toPos)
        {
            var snakeCollidedWithItself = _snake.CheckCollisionAtPosition(toPos);
            var snakeCollideWithWalls = Level.Walls.Any(w => _snake.CheckCollisionAtPosition(w));
            var inPosWall = Level.Walls.Any(w => w == toPos);

            if (snakeCollidedWithItself || snakeCollideWithWalls || inPosWall)
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
