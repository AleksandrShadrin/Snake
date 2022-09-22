using Snake.Presentation.Base;

namespace Snake.Presentation.Scenes
{
    public class Exit : BaseScene
    {
        public Action ExitFromApp { get; set; }

        private readonly InputHandler _inputHandler;
        public Exit(InputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }
        public override void DoOnKeyPressed()
        {
            if(_inputHandler.ConsoleKey.HasValue)
            {
                ExitFromApp?.Invoke();
                Console.Clear();
            }
        }

        public override void Render()
        {
            Console.WriteLine("Press any key.");
        }

        public override void StartScene()
        {
            _inputHandler.ClearConsoleKeyInfo();
            Console.Clear();
            Render();
        }
    }
}
