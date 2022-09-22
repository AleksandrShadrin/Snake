namespace Snake.Presentation
{
    public class InputHandler
    {
        public ConsoleKey? ConsoleKey { get; private set; }
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

        public void ClearConsoleKeyInfo()
        {
            ConsoleKey = null;
        }

        private void ReadConsoleInput()
        {
            while (handling)
            {
                var keyInfo = Console.ReadKey(true);
                ConsoleKey = keyInfo.Key;
                OnChange?.Invoke();
            }
        }

    }
}
