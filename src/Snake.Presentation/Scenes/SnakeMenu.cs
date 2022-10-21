using Snake.Application.Adapters;
using Snake.Presentation.Base;
using Snake.Presentation.Constants;
using Snake.Presentation.LevelGenerator;

namespace Snake.Presentation.Scenes
{
    public class SnakeMenu : BaseScene
    {
        private readonly InputHandler _inputHandler;
        private readonly ISnakeGameService _gameService;
        private readonly ILevelGenerator _levelGenerator;

        private MenuCases selectedMenuCase = MenuCases.START_GAME;

        public SnakeMenu(InputHandler inputHandler, ISnakeGameService gameService, ILevelGenerator levelGenerator)
        {
            _inputHandler = inputHandler;
            _gameService = gameService;
            _levelGenerator = levelGenerator;
        }

        public override void Render()
        {
            Console.Clear();
            Enum
                .GetValues(typeof(MenuCases))
                .Cast<MenuCases>()
                .ToList().ForEach((mc) =>
                {
                    if (mc == selectedMenuCase)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(mc.ToString());
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(mc.ToString());
                    }
                });
        }

        public override void DoOnKeyPressed()
        {
            var maxValue = Enum
                .GetValues(typeof(MenuCases))
                .Cast<MenuCases>()
                .Select(v => (int)v)
                .Max();
            var key = _inputHandler.ConsoleKey;

            SelectNextMenuCase(maxValue, key.Value);
            Render();

            if (key == ConsoleKey.Enter)
            {
                switch (selectedMenuCase)
                {
                    case MenuCases.START_GAME:
                        _gameService.CreateGame(_levelGenerator.GenerateLevel().Item1,
                            _levelGenerator.GenerateLevel().Item2);
                        SwitchScene?.Invoke(typeof(SnakeGame));
                        break;
                    case MenuCases.CONTINUE_GAME:
                        SwitchScene?.Invoke(typeof(SnakeGame));
                        break;
                    case MenuCases.LOAD_GAME:
                        SwitchScene?.Invoke(typeof(LoadScene));
                        break;
                    case MenuCases.SAVE_GAME:
                        SwitchScene?.Invoke(typeof(SaveScene));
                        break;
                    case MenuCases.EXIT:
                        SwitchScene?.Invoke(typeof(Exit));
                        break;
                    default:
                        break;
                }
            }

            if (key == ConsoleKey.Escape)
            {
                SwitchScene?.Invoke(typeof(SnakeGame));
            }
        }

        private void SelectNextMenuCase(int maxValue, ConsoleKey key)
        {
            if (key == ConsoleKey.DownArrow)
            {
                if ((int)selectedMenuCase + 1 > maxValue)
                {
                    selectedMenuCase = (MenuCases)1;
                }
                else
                {
                    selectedMenuCase = (MenuCases)(int)(selectedMenuCase + 1);
                }
            }

            if (key == ConsoleKey.UpArrow)
            {
                if ((int)selectedMenuCase - 1 < 1)
                {
                    selectedMenuCase = (MenuCases)maxValue;
                }
                else
                {
                    selectedMenuCase = (MenuCases)(int)(selectedMenuCase - 1);
                }
            }
        }

        public override async Task StartScene()
        {
            Render();

            while (Selected)
            {
                await Task.Delay(200);
            }
        }
    }
}
