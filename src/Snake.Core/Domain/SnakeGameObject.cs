using Snake.Core.Constants;
using Snake.Core.Exceptions;
using Snake.Core.ValueObjects;

namespace Snake.Core.Domain
{
    /// <summary>
    /// Класс отображает змейку, как игровой объект
    /// </summary>
    public class SnakeGameObject
    {
        private SnakeLifeState snakeLife = SnakeLifeState.LIVE;
        private LinkedList<PosXY> snakeBody;

        public SnakeGameObject(LinkedList<PosXY> snakeBody)
        {
            this.snakeBody = snakeBody;
        }

        public SnakeGameObject(PosXY position)
        {
            if (position is null)
            {
                throw new TrySetNullPositionException();
            }

            snakeBody = new();
            snakeBody.AddFirst(position);
        }

        /// <summary>
        /// Проверяет состояние жизни змейки
        /// </summary>
        /// <returns> Возвращает true, если змейка жива </returns>
        public bool SnakeIsAlive()
        {
            return snakeLife == SnakeLifeState.LIVE;
        }

        /// <summary>
        /// Обратная функция SnakeIsAlive
        /// </summary>
        public bool SnakeIsDead()
        {
            return !SnakeIsAlive();
        }

        /// <summary>
        /// Меняет состояние объекта на DEAD
        /// </summary>
        public void KillSnake()
        {
            snakeLife = SnakeLifeState.DEAD;
        }

        /// <summary>
        /// Проверяет столкновение в некоторой координате с телом змейки
        /// </summary>
        /// <param name="position">PosXY, в которой необходимо проверить столкновение</param>
        /// <returns>true, если столкновение в точке есть</returns>
        public bool CheckCollisionAtPosition(PosXY position)
        {
            var collided = snakeBody.Any(i => i.Equals(position));
            return collided;
        }

        /// <summary>
        /// Передвигает змейку в определенную точку
        /// </summary>
        /// <param name="toPos">точка в которую будет совершенно перемещение</param>
        public void Move(PosXY toPos)
        {
            if (SnakeIsDead())
            {
                throw new TryMoveWhenDeadException();
            }

            snakeBody.RemoveFirst();
            snakeBody.AddLast(toPos);
        }

        /// <summary>
        /// Увеличивает размер тела змейки
        /// </summary>
        public void IncreaseSnake()
        {
            snakeBody.AddFirst(snakeBody.First.Value);
        }

        /// <summary>
        /// Возвращает голову змейки
        /// </summary>
        /// <returns>Возвращает объект PosXY - позицию в которой находится голова</returns>
        public PosXY GetHead()
        {
            return snakeBody.Last.Value;
        }

        /// <summary>
        /// Возвращает тело змейки (без хвоста и головы)
        /// </summary>
        /// <returns>Может вернуть пустую коллекцию</returns>
        public IEnumerable<PosXY> GetBody()
        {
            return snakeBody.Where(
                i => i != GetHead()
                     && i != GetTail());
        }

        /// <summary>
        /// Возвращает хвост змейки
        /// </summary>
        /// <returns>Возвращает хвост, если он есть, иначе null</returns>
        public PosXY? GetTail()
        {
            var snakeTail = snakeBody.First.Value;
            if (GetHead() != snakeBody.First.Value)
            {
                return snakeBody.First.Value;
            }

            return null;
        }
    }
}
