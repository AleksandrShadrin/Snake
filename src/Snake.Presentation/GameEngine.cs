using Snake.Application.Adapters;
using Snake.Application.Models;
using Snake.Core.Constants;
using Snake.Core.ValueObjects;
using Snake.Presentation.Services;

namespace Snake.Presentation
{
    public class GameEngine
    {
        private ISnakeGameService _snakeGameService;
        private InputHandler _handler = new();
        private double gameSpeed = 4;
        private readonly IGameSnakeRenderService _renderService;
        public GameEngine(ISnakeGameService snakeGameService, IGameSnakeRenderService renderService)
        {
            _snakeGameService = snakeGameService;
            _renderService = renderService;
        }
        public void Start(PosXY pos, Level level)
        {
            Console.CursorVisible = false;
            _snakeGameService.CreateGame(pos, level);
            _handler.StartHandleConsoleInput();
            _handler.OnChange += () =>
            {
                OnKeyPressed();
            };
            var gameLoop = Task.Factory.StartNew(Run);
            gameLoop.Wait();
            Console.Clear();
            Console.WriteLine("Game Over");
        }

        private void Render()
        {
            var snake = _snakeGameService.GetSnakeBody();
            var walls = _snakeGameService.GetWalls();
            var rewards = _snakeGameService.GetRewards();

            Console.Clear();

            _renderService.RenderSnakeBodyObject(snake);
            _renderService.RenderWalls(walls);
            _renderService.RenderRewardObjects(rewards);
        }
        private void OnKeyPressed()
        {
            var key = _handler.ConsoleKeyInfo.Key;

            Direction direction = key switch
            {
                ConsoleKey.DownArrow => Direction.BOTTOM,
                ConsoleKey.UpArrow => Direction.TOP,
                ConsoleKey.LeftArrow => Direction.LEFT,
                ConsoleKey.RightArrow => Direction.RIGHT,
            };

            try
            {
                _snakeGameService.ChangeMoveDirection(direction);
            }
            catch (Exception ex)
            {

            }
        }
        private void GenerateReward()
        {
            Random rnd = new();
            if (rnd.NextDouble() > 0.85)
            {
                _snakeGameService.AddRewardObject(new(new(rnd.Next(20), rnd.Next(20)), 2));
            }
        }
        private void Run()
        {
            while (!_snakeGameService.GameIsOver())
            {
                GenerateReward();
                _snakeGameService.MoveSnake();
                Render();
                Thread.Sleep((int)(500 / gameSpeed));
            }
        }
    }
}
