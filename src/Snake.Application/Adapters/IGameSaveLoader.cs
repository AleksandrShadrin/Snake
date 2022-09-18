using Snake.Application.Models;

namespace Snake.Application.Adapters
{
    public interface IGameSaveLoader
    {
        IEnumerable<string> GetSaveFiles();
        void SaveGame();
        void LoadGame(string fileName);
    }
}
