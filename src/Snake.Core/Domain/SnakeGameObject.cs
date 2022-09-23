﻿using Snake.Core.Constants;
using Snake.Core.Exceptions;
using Snake.Core.ValueObjects;

namespace Snake.Core.Domain
{
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

        public bool CheckCollisionAtPosition(PosXY position)
        {
            var collided = snakeBody.Any(i => i.Equals(position));
            return collided;
        }
        public void Move(PosXY toPos)
        {
            if (SnakeIsDead())
            {
                throw new TryMoveWhenDeadException();
            }

            snakeBody.RemoveFirst();
            snakeBody.AddLast(toPos);
        }

        public void IncreaseSnake()
        {
            snakeBody.AddFirst(snakeBody.First.Value);
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
            if (GetHead() != snakeBody.First.Value)
            {
                return snakeBody.First.Value;
            }
            return null;
        }
    }
}
