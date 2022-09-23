using Snake.Application.Adapters;
using Snake.Presentation.Base;

namespace Snake.Presentation.Scenes
{
    public class LoadScene : BaseScene
    {
        private readonly IGameSaveLoader saveLoader;
        private readonly InputHandler inputHandler;

        private int filesPerPageCount;
        private int currentPage = 0;
        private int selectedElementInList = 0;
        private List<string> fileNames;
        private List<string> FilesOnCurrentPage()
            => fileNames
                .Skip(currentPage * filesPerPageCount)
                .Take(filesPerPageCount)
                .ToList();

        public LoadScene(InputHandler inputHandler, IGameSaveLoader saveLoader, int filesPerPageCount = 10)
        {
            this.inputHandler = inputHandler;
            this.saveLoader = saveLoader;
            this.filesPerPageCount = filesPerPageCount;
        }

        public override void DoOnKeyPressed()
        {
            if (inputHandler.ConsoleKey == ConsoleKey.Escape)
            {
                OnSwitchScene?.Invoke(typeof(SnakeMenu));
            }

            if (inputHandler.ConsoleKey == ConsoleKey.Enter)
            {
                saveLoader.LoadGame(fileNames[currentPage * filesPerPageCount + selectedElementInList]);
                OnSwitchScene?.Invoke(typeof(SnakeGame));
            }

            if (inputHandler.ConsoleKey == ConsoleKey.LeftArrow)
            {
                currentPage = Math.Max(0, currentPage - 1);
                selectedElementInList = 0;
            }
            if (inputHandler.ConsoleKey == ConsoleKey.RightArrow)
            {
                selectedElementInList = 0;
                currentPage = Math.Min(fileNames.Count / filesPerPageCount, currentPage + 1);
            }

            MoveSelectedElementInList();
        }

        private void MoveSelectedElementInList()
        {
            if (inputHandler.ConsoleKey == ConsoleKey.DownArrow)
            {
                selectedElementInList = Math.Min(FilesOnCurrentPage().Count - 1, selectedElementInList + 1);
            }
            if (inputHandler.ConsoleKey == ConsoleKey.UpArrow)
            {
                selectedElementInList = Math.Max(0, selectedElementInList - 1);
            }
        }

        public override void Render()
        {
            var filesOnCurrentPage = FilesOnCurrentPage();

            Console.Clear();

            for (int i = 0; i < filesOnCurrentPage.Count; i++)
            {
                if (i == selectedElementInList)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{i + 1}.{filesOnCurrentPage[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"{i + 1}.{filesOnCurrentPage[i]}");
                }
            }

            Console.WriteLine("{0,8}", $"{currentPage + 1}/{fileNames.Count / filesPerPageCount + 1}");
        }

        public override async Task StartScene()
        {
            fileNames = saveLoader.GetSaveFiles().ToList();

            while (Selected)
            {
                Render();
                await Task.Delay(200);
            }
        }
    }
}
