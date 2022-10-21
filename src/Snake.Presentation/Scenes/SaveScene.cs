using Snake.Application.Adapters;
using Snake.Presentation.Base;

namespace Snake.Presentation.Scenes
{
    public class SaveScene : BaseScene
    {
        private readonly IGameSaveLoader _saveLoader;
        private readonly InputHandler _inputHandler;

        private bool _dataIsSaved = false;

        public SaveScene(IGameSaveLoader saveLoader, InputHandler inputHandler)
        {
            _saveLoader = saveLoader;
            _inputHandler = inputHandler;
        }

        public override void DoOnKeyPressed()
        {
            if (_dataIsSaved)
            {
                _dataIsSaved = false;
                SwitchScene?.Invoke(typeof(SnakeMenu));
                return;
            }

            if (_inputHandler.ConsoleKey == ConsoleKey.Y && _dataIsSaved is false)
            {
                _saveLoader.SaveGame();
                _dataIsSaved = true;
                Console.Clear();
                var savedFile = _saveLoader.GetSaveFiles().LastOrDefault();
                Console.WriteLine($"Game data was saved in {savedFile} file.");
                Console.WriteLine("Press any key to exit from save menu.");
            }
            else if (_inputHandler.ConsoleKey == ConsoleKey.N && _dataIsSaved is false)
            {
                SwitchScene?.Invoke(typeof(SnakeMenu));
            }
        }

        public override void Render()
        {
            Console.Clear();
            Console.WriteLine("Do you want to save this game? [Y/N]");
        }

        public override async Task StartScene()
        {
            Render();
            while (Selected)
            {
                await Task.Delay(100);
            }
        }
    }
}
