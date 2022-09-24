using Snake.Presentation.Base;
using Snake.Presentation.Controller;

namespace Snake.Presentation.Scenes
{
    public class Exit : BaseScene
    {
        private readonly InputHandler _inputHandler;
        private bool buttonPressed = false;
        private readonly IController _controller;

        public Exit(InputHandler inputHandler, IController controller)
        {
            _inputHandler = inputHandler;
            _controller = controller;
        }
        public override void DoOnKeyPressed()
        {
            if(_inputHandler.ConsoleKey.HasValue)
            {
                _controller.Stop();
                buttonPressed = true;
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
