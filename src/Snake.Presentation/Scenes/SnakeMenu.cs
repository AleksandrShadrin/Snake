using Snake.Application.Adapters;
using Snake.Presentation.Base;
using Snake.Presentation.Constants;
using Snake.Presentation.LevelGenerator;

namespace Snake.Presentation.Scenes
{
    public class SnakeMenu : BaseScene
    {
        private readonly InputHandler inputHandler;
        private readonly ISnakeGameService gameService;
        private readonly ILevelGenerator levelGenerator;

        private MenuCases selectedMenuCase = MenuCases.START_GAME;
        private bool MenuIsInvoked = false;

        public SnakeMenu(InputHandler inputHandler, ISnakeGameService gameService, ILevelGenerator levelGenerator)
        {
            this.inputHandler = inputHandler;
            this.gameService = gameService;
            this.levelGenerator = levelGenerator;
        }

        public void InvokeMenu()
        {
            MenuIsInvoked = true;
        }

        public void CloseMenu()
        {
            MenuIsInvoked = false;
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
            var key = inputHandler.ConsoleKey;

            SelectNextMenuCase(maxValue, key.Value);

            if (key == ConsoleKey.Enter)
            {
                switch (selectedMenuCase)
                {
                    case MenuCases.START_GAME:
                        gameService.CreateGame(levelGenerator.GenerateLevel().Item1, levelGenerator.GenerateLevel().Item2);
                        OnSwitchScene?.Invoke(typeof(SnakeGame));
                        break;
                    case MenuCases.CONTINUE_GAME:
                        OnSwitchScene?.Invoke(typeof(SnakeGame));
                        break;
                    case MenuCases.LOAD_GAME:
                        OnSwitchScene?.Invoke(typeof(LoadScene));
                        break;
                    case MenuCases.SAVE_GAME:
                        OnSwitchScene?.Invoke(typeof(SaveScene));
                        break;
                    case MenuCases.EXIT:
                        OnSwitchScene?.Invoke(typeof(Exit));
                        break;
                    default:
                        break;
                }
            }

            if (key == ConsoleKey.Escape)
            {
                OnSwitchScene?.Invoke(typeof(SnakeGame));
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

        public override void StartScene()
        {
            inputHandler.ClearConsoleKeyInfo();
            while (Selected)
            {
                Render();
                Thread.Sleep(200);
            }
        }
    }
}
