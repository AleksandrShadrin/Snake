using Snake.Core.Exceptions;

namespace Snake.Core.Domain
{
    public class SnakeGameState
    {
        public uint Score { get; private set; }

        private Predicate<SnakeGameState> Conditions;
        private SnakeGameObject _snake;
        public SnakeGameState(SnakeGameObject snake)
        {
            if (snake is null)
            {
                throw new SnakeIsEmptyException();
            }
            _snake = snake;
        }

        public SnakeGameState AddGameOverConditions(Predicate<SnakeGameState> condition)
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
