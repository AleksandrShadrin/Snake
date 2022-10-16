using Snake.Core.Constants;
using Snake.Core.Exceptions;
using Snake.Core.ValueObjects;

namespace Snake.Core.Domain
{
    /// <summary>
    /// Игровой менеджер, управляет состоянием игры, а также игровыми объектами (змейка, награды, стены)
    /// </summary>
    public class SnakeGameManager
    {
        public uint Score { get; private set; }

        public Direction MoveDirection { get; private set; } =
            Direction.RIGHT; // Змейка начинает движение всегда вправо

        public Level Level { get; private set; }

        private bool snakeCanChangeDirection = true;
        private Predicate<SnakeGameManager> conditions;
        private readonly SnakeGameObject _snake;
        private readonly List<RewardObject> RewardObjects = new();

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

        /// <summary>
        /// Меняет направление змейки
        /// </summary>
        /// <param name="direction"> enum class, в котором заданы возможные направления</param>
        /// <exception cref="TryChangeSnakeDirectionMoreThenOncePerMoveException"> исключение, когда пытаются поменять направление > 1 раза за перемещение змейки</exception>
        /// <exception cref="WrongDirectionException"> исключение, когда неверно изменяется направление</exception>
        public void ChangeDirection(Direction direction)
        {
            if (snakeCanChangeDirection is false)
                throw new TryChangeSnakeDirectionMoreThenOncePerMoveException();

            if (Math.Abs((int)direction) == Math.Abs((int)MoveDirection))
            {
                throw new WrongDirectionException();
            }

            snakeCanChangeDirection = false;
            MoveDirection = direction;
        }

        /// <summary>
        /// Возвращает копию наград
        /// </summary>
        /// <returns> Возвращает readonly list</returns>
        public IReadOnlyList<RewardObject> GetRewardObjects()
            => RewardObjects.Select(ro => ro with { }).ToList();

        /// <summary>
        /// Удаляет награду
        /// </summary>
        /// <param name="rewardObject"> награда, которая должна совпадать по значениям с какой-либо уже существующей наградой, чтобы быть удаленной</param>
        public void RemoveRewardObject(RewardObject rewardObject)
            => RewardObjects.Remove(rewardObject);

        /// <summary>
        /// Добавляет объект награды, если в его позиции уже есть данный объект, то происходит замена на новый
        /// </summary>
        /// <param name="rewardObject">
        /// RewardObject, который будет добавлен, если в позиции в которую он добавляется
        /// не существует стены и награды, если в этой позиции уже есть RewardObject, то происходит
        /// замена старого на новый, а если стена, то создается исключение
        /// </param>
        /// <exception cref="RewardObjectCantBeInWallPosException">
        /// исключение, когда награду пытаются добавить в позицию, где находится стена
        /// </exception>
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

        /// <summary>
        /// Добавляет условие для окончания игры
        /// </summary>
        /// <param name="condition">Predicate, в параметрах которого SnakeGameManager</param>
        public SnakeGameManager AddGameOverConditions(Predicate<SnakeGameManager> condition)
        {
            conditions += condition;
            return this;
        }

        /// <summary>
        /// Проверка окончания игры
        /// </summary>
        /// <returns> Возвращает true, если игра завершена</returns>
        public bool GameIsOver()
        {
            var result = conditions?.Invoke(this);
            if (result is { })
            {
                return _snake.SnakeIsDead() || result.Value;
            }

            return _snake.SnakeIsDead();
        }

        /// <summary>
        /// Добавляет очки
        /// </summary>
        /// <param name="points"> положительное целое число, которое будет добавлено ко всем очкам</param>
        public void AddScore(uint points)
        {
            Score += points;
        }

        /// <summary>
        /// Перемещает змейку в следующую позицию
        /// </summary>
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
                Direction.TOP => prevPos with
                {
                    X = prevPos.X, Y = (prevPos.Y - 1) < 0 ? Level.GameSize.Y : prevPos.Y - 1
                },
                Direction.BOTTOM => prevPos with
                {
                    X = prevPos.X, Y = (prevPos.Y + 1) >= Level.GameSize.Y ? 0 : prevPos.Y + 1
                },
                Direction.RIGHT => prevPos with
                {
                    X = (prevPos.X + 1) >= Level.GameSize.X ? 0 : prevPos.X + 1, Y = prevPos.Y
                },
                Direction.LEFT => prevPos with
                {
                    X = (prevPos.X - 1) < 0 ? Level.GameSize.X : prevPos.X - 1, Y = prevPos.Y
                },
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
            var collidedObjects = RewardObjects.Where(i => _snake.CheckCollisionAtPosition(i.Position))
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
