using Snake.Application.Exceptions;
using Snake.Application.Models;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Snake.Application.Adapters
{
    public class JsonGameSaveLoader : IGameSaveLoader
    {
        private readonly ISnakeGameService _snakeGameService;
        private Regex _format = new(@"^[\d]{3}-[\d]{3}-[\d]{3}\.json$");
        private readonly string saveFolder = Path.Combine(".", "Saves");

        public JsonGameSaveLoader(ISnakeGameService snakeGameService)
        {
            _snakeGameService = snakeGameService;
        }

        public IEnumerable<string> GetSaveFiles()
        {
            return Directory
                .GetFiles(saveFolder)
                .Select(f => f.Split(Path.DirectorySeparatorChar).Last())
                .Where(f => _format.IsMatch(f));
        }

        public void LoadGame(string fileName)
        {
            FileInfo file = new FileInfo(Path.Combine(saveFolder, fileName));
            if (file.Exists)
            {
                using(Stream sr = file.OpenRead())
                {
                    var data = JsonSerializer.Deserialize<SnakeGameData>(sr);
                    if(data is { })
                    {
                        _snakeGameService.LoadGame(data);
                    }
                    else
                    {
                        throw new InvalidJsonDeserializationException(file.FullName);
                    }
                }
            }
            else
            {
                throw new SaveFileDontExistException(file.FullName);
            }
        }

        public void SaveGame()
        {
            var newFileName = GenerateFileName();
            var data = new
                SnakeGameData(_snakeGameService.GetSnakeBody(),
                _snakeGameService.GetRewards().ToList(),
                _snakeGameService.GetLevel(), 
                _snakeGameService.CurrentDirection());

            using (var stream = new FileStream(Path.Combine(".", "Saves", newFileName), FileMode.Create))
            {
                JsonSerializer.Serialize(stream, data);
            }
        }

        private string GenerateFileName()
        {
            var defaultFileName = "000-000-000.json";

            if (Directory.Exists(saveFolder) is false)
            {
                Directory.CreateDirectory(saveFolder);
            }

            if (Directory.GetFiles(saveFolder).Length == 0)
            {
                return defaultFileName;
            }

            var maxFileNumber = GetSaveFiles()
                .Select(f => f.Split(".")[0]
                    .Split("-"))
                .Select(fa => 1 + Int32.Parse(String.Join("", fa)))
                .Max().ToString("D9");

            return maxFileNumber[0..3] + '-' + maxFileNumber[3..6] + '-' + maxFileNumber[6..9] + ".json";
        }
    }
}
