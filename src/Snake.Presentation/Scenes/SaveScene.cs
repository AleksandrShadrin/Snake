using Snake.Application.Adapters;
using Snake.Presentation.Base;

namespace Snake.Presentation.Scenes
{
    public class SaveScene : BaseScene
    {
        private readonly IGameSaveLoader saveLoader;
        private readonly InputHandler inputHandler;

        private bool dataIsSaved = false;

        public SaveScene(IGameSaveLoader saveLoader, InputHandler inputHandler)
        {
            this.saveLoader = saveLoader;
            this.inputHandler = inputHandler;
        }

        public override void DoOnKeyPressed()
        {
            if (dataIsSaved)
            {
                dataIsSaved = false;
                OnSwitchScene?.Invoke(typeof(SnakeMenu));
                return;
            }

            if (inputHandler.ConsoleKey == ConsoleKey.Y && dataIsSaved is false)
            {
                saveLoader.SaveGame();
                dataIsSaved = true;
                Console.Clear();
                var savedFile = saveLoader.GetSaveFiles().LastOrDefault();
                Console.WriteLine($"Game data was saved in {savedFile} file.");
                Console.WriteLine("Press any key to exit from save menu.");
            }
            else if(inputHandler.ConsoleKey == ConsoleKey.N && dataIsSaved is false)
            {
                OnSwitchScene?.Invoke(typeof(SnakeMenu));
            }


        }

        public override void Render()
        {
            Console.Clear();
            Console.WriteLine("Do you want to save this game? [Y/N]");
        }

        public override void StartScene()
        {
            inputHandler.ClearConsoleKeyInfo();
            Render();
        }
    }
}
