using Snake.Application.Adapters;
using Snake.Presentation.Base;

namespace Snake.Presentation.Scenes
{
    public class LoadScene : BaseScene
    {
        private readonly IGameSaveLoader _saveLoader;
        private readonly InputHandler _inputHandler;

        private int _filesPerPageCount;
        private int _currentPage = 0;
        private int _selectedElementInList = 0;
        private List<string> _fileNames;

        private List<string> FilesOnCurrentPage()
            => _fileNames
                .Skip(_currentPage * _filesPerPageCount)
                .Take(_filesPerPageCount)
                .ToList();

        public LoadScene(InputHandler inputHandler, IGameSaveLoader saveLoader, int filesPerPageCount = 10)
        {
            _inputHandler = inputHandler;
            _saveLoader = saveLoader;
            _filesPerPageCount = filesPerPageCount;
        }

        public override void DoOnKeyPressed()
        {
            if (_inputHandler.ConsoleKey == ConsoleKey.Escape)
            {
                SwitchScene?.Invoke(typeof(SnakeMenu));
            }

            if (_inputHandler.ConsoleKey == ConsoleKey.Enter)
            {
                if (_fileNames.Count() == 0)
                {
                    SwitchScene?.Invoke(typeof(SnakeMenu));
                    return;
                }

                _saveLoader.LoadGame(_fileNames[_currentPage * _filesPerPageCount + _selectedElementInList]);
                SwitchScene?.Invoke(typeof(SnakeGame));
            }

            if (_inputHandler.ConsoleKey == ConsoleKey.LeftArrow)
            {
                _currentPage = Math.Max(0, _currentPage - 1);
                _selectedElementInList = 0;
            }

            if (_inputHandler.ConsoleKey == ConsoleKey.RightArrow)
            {
                _selectedElementInList = 0;
                _currentPage = Math.Min(_fileNames.Count / _filesPerPageCount, _currentPage + 1);
            }

            MoveSelectedElementInList();
        }

        private void MoveSelectedElementInList()
        {
            if (_inputHandler.ConsoleKey == ConsoleKey.DownArrow)
            {
                _selectedElementInList = Math.Min(FilesOnCurrentPage().Count - 1, _selectedElementInList + 1);
            }

            if (_inputHandler.ConsoleKey == ConsoleKey.UpArrow)
            {
                _selectedElementInList = Math.Max(0, _selectedElementInList - 1);
            }
        }

        public override void Render()
        {
            var filesOnCurrentPage = FilesOnCurrentPage();

            Console.Clear();

            for (int i = 0; i < filesOnCurrentPage.Count; i++)
            {
                if (i == _selectedElementInList)
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

            Console.WriteLine("{0,8}", $"{_currentPage + 1}/{_fileNames.Count / _filesPerPageCount + 1}");
        }

        public override async Task StartScene()
        {
            _fileNames = _saveLoader.GetSaveFiles().ToList();

            while (Selected)
            {
                Render();
                await Task.Delay(200);
            }
        }
    }
}
