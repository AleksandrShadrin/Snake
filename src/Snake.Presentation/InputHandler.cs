namespace Snake.Presentation
{
    public class InputHandler
    {
        public ConsoleKeyInfo ConsoleKeyInfo { get; private set; }
        public Action OnChange { get; set; }

        private bool handling;
        public void StartHandleConsoleInput()
        {
            handling = true;
            var task = Task.Factory.StartNew(ReadConsoleInput);
        }

        public void StopHandling()
        {
            handling = false;
        }

        private void ReadConsoleInput()
        {
            while (handling)
            {
                var key = Console.ReadKey(true);
                ConsoleKeyInfo = key;
                OnChange?.Invoke();
            }
        }

    }
}
