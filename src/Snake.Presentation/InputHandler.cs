namespace Snake.Presentation
{
    public class InputHandler
    {
        public ConsoleKey? ConsoleKey { get; private set; }

        /// <summary>
        /// Вызывается при нажатии на клавишу
        /// </summary>
        public Action OnChange { get; set; }

        public void StartHandleConsoleInput()
        {
            var task = Task.Factory.StartNew(ReadConsoleInput);
        }

        /// <summary>
        /// Очищает значение ConsoleKey
        /// </summary>
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
