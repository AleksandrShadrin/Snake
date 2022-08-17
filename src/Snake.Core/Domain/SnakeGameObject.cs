﻿using Snake.Core.Exceptions;
using Snake.Core.ValueObjects;

namespace Snake.Core.Domain
{
    public class SnakeGameObject
    {

        public Direction Direction { get; private set; } = Direction.RIGHT;

        private SnakeLifeState snakeLife = SnakeLifeState.LIVE;
        private LinkedList<PosXY> snakeBody = new();

        public SnakeGameObject(PosXY position)
        {
            if (position is null)
            {
                throw new TrySetNullPositionException();
            }
            snakeBody.AddFirst(position);
        }

        public bool SnakeIsAlive()
        {
            return snakeLife == SnakeLifeState.LIVE;
        }

        public bool SnakeIsDead()
        {
            return !SnakeIsAlive();
        }

        public void KillSnake()
        {
            snakeLife = SnakeLifeState.DEAD;
        }

        public void ChangeDirection(Direction direction)
        {
            if ((int)direction == -(int)this.Direction)
            {
                throw new WrongDirectionException();
            }
            Direction = direction;
        }

        public bool CollideWithObjectAtPos(PosXY pos)
        {
            var collided = snakeBody.SingleOrDefault(i => i.Equals(pos));
            return collided is { };
        }

        public void Move(PosXY toPos)
        {
            if (SnakeIsDead())
            {
                throw new TryMoveWhenDeadException();
            }

            if (CollideWithObjectAtPos(toPos))
            {
                KillSnake();
                return;
            }

            snakeBody.RemoveFirst();
            snakeBody.AddLast(toPos);
        }

        public void IncreaseSnake()
        {
            snakeBody.AddFirst(snakeBody.First);
        }

        public PosXY GetHead()
        {
            return snakeBody.Last.Value;
        }

        // Can be empty
        public IEnumerable<PosXY> GetBody()
        {
            return snakeBody.Where(
               i => i != GetHead()
                && i != GetTail());
        }

        // Can be null
        public PosXY GetTail()
        {
            var snakeTail = snakeBody.First.Value;
            if (snakeTail != GetBody() && snakeTail != GetHead())
            {
                return snakeTail;
            }
            return null;
        }
    }
}
