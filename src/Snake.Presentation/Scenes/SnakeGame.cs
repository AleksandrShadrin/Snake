using Snake.Application.Adapters;
using Snake.Core.Constants;
using Snake.Core.Exceptions;
using Snake.Core.ValueObjects;
using Snake.Presentation.Base;
using Snake.Presentation.LevelGenerator;
using Snake.Presentation.Services;

namespace Snake.Presentation.Scenes
{
    public class SnakeGame : BaseScene
    {
        private readonly IGameSnakeRenderService _renderService;
        private readonly ISnakeGameService _snakeGameService;
        private readonly InputHandler _handler;
        private readonly ILevelGenerator _levelGenerator;

        private double gameSpeed = 4;
        private Random valueGenerator = new Random();

        public SnakeGame(ISnakeGameService snakeGameService, IGameSnakeRenderService renderService, InputHandler handler, ILevelGenerator levelGenerator)
        {
            _snakeGameService = snakeGameService;
            _renderService = renderService;
            _handler = handler;
            _levelGenerator = levelGenerator;
        }

        public override void Render()
        {
            var snake = _snakeGameService.GetSnakeBody();
            var walls = _snakeGameService.GetWalls();
            var rewards = _snakeGameService.GetRewards();

            Console.Clear();
            Console.CursorVisible = false;

            _renderService.RenderSnakeBodyObject(snake, _snakeGameService.CurrentDirection());
            _renderService.RenderWalls(walls);
            _renderService.RenderRewardObjects(rewards);
        }

        private void GenerateReward()
        {
            if (valueGenerator.NextDouble() > 0.85)
            {
                var rndPos = new PosXY(
                    valueGenerator.Next(_snakeGameService.GetLevel().GameSize.X - 1),
                    valueGenerator.Next(_snakeGameService.GetLevel().GameSize.Y - 1));

                _snakeGameService.AddRewardObject(new(rndPos, 2));
            }
        }

        public override void DoOnKeyPressed()
        {
            var key = _handler.ConsoleKeyInfo.Key;

            Direction? direction = key switch
            {
                ConsoleKey.DownArrow => Direction.BOTTOM,
                ConsoleKey.UpArrow => Direction.TOP,
                ConsoleKey.LeftArrow => Direction.LEFT,
                ConsoleKey.RightArrow => Direction.RIGHT,
                _ => null
            };

            if (direction.HasValue)
            {
                try
                {
                    _snakeGameService.ChangeMoveDirection(direction.Value);
                    return;
                }
                catch (SnakeException ex)
                {
                    
                }
            }

            if(key == ConsoleKey.Escape)
            {
                OnSwitchScene?.Invoke(nameof(SnakeMenu));
            }

            if(key == ConsoleKey.Enter && _snakeGameService.GameIsOver())
            {
                _snakeGameService.CreateGame(_levelGenerator.GenerateLevel().Item1, _levelGenerator.GenerateLevel().Item2);
                OnSwitchScene?.Invoke(nameof(SnakeGame));
            }
        }

        public override void StartScene()
        {
            while (!_snakeGameService.GameIsOver() && Selected)
            {
                GenerateReward();
                _snakeGameService.MoveSnake();
                Render();
                Thread.Sleep((int)(500 / gameSpeed));
            }
            if (_snakeGameService.GameIsOver())
            {
                Console.Clear();
                Console.WriteLine($"Game is over your score is {_snakeGameService.GetScore()}.");
                Console.WriteLine("Press enter to start again.");
            }
        }
    }
}
