namespace Snake.Application.Adapters
{
    public interface IGameSaveLoader
    {
        /// <summary>
        /// Возвращает файлы сохранений
        /// </summary>
        /// <returns> Возвращает IEnumerable&lt;string&gt; - коллекцию имен файлов</returns>
        IEnumerable<string> GetSaveFiles();

        /// <summary>
        /// Сохраняет игру
        /// </summary>
        void SaveGame();

        /// <summary>
        /// Загружает игру
        /// </summary>
        void LoadGame(string fileName);
    }
}
