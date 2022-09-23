namespace Snake.Presentation
{
    public class InputHandler
    {
        public ConsoleKey? ConsoleKey { get; private set; }
        public Action OnChange { get; set; }

        public void StartHandleConsoleInput()
        {
            var task = Task.Factory.StartNew(ReadConsoleInput);
        }

        public void ClearConsoleKeyInfo()
        {
            ConsoleKey = null;
        }

        private void ReadConsoleInput()
        {
            Console.CursorVisible = false;
            while (true)
            {
                ConsoleKey = Console.ReadKey(true).Key;
                OnChange?.Invoke();
            }
        }
    }
}
