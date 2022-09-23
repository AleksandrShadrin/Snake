using Snake.Presentation.Base;

namespace Snake.Presentation.Scenes
{
    public class Exit : BaseScene
    {
        public Action ExitFromApp { get; set; }

        private readonly InputHandler _inputHandler;
        private bool buttonPressed = false;
        public Exit(InputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }
        public override void DoOnKeyPressed()
        {
            if(_inputHandler.ConsoleKey.HasValue)
            {
                ExitFromApp?.Invoke();
                buttonPressed = true;
                Console.Clear();
            }
        }

        public override void Render()
        {
            Console.WriteLine("Press any key.");
        }

        public override async Task StartScene()
        {
            Console.Clear();
            Render();
            while(buttonPressed is false)
            {
                await Task.Delay(100);
            }
        }
    }
}
