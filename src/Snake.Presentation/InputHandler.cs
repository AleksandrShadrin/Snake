namespace Snake.Presentation
{
    public class InputHandler
    {
        public ConsoleKeyInfo ConsoleKeyInfo { get; private set; }
        public Action OnChange { get; set; }
        public void StartHandleConsoleInput()
        {
            var task = Task.Factory.StartNew(ReadConsoleInput);
        }

        private void ReadConsoleInput()
        {
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key is ConsoleKey.DownArrow or ConsoleKey.LeftArrow or ConsoleKey.RightArrow or ConsoleKey.UpArrow && key.Key != ConsoleKeyInfo.Key)
                {
                    ConsoleKeyInfo = key;
                    OnChange?.Invoke();
                }
            }
        }

    }
}
