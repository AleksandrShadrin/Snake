using Shouldly;
using Snake.Application.Adapters;
using Snake.Core.Factories;
using Snake.Core.ValueObjects;

namespace Snake.Application.Tests
{
    public class JsonGameSaveLoaderTests
    {
        [Fact]
        public void Save_Method_Should_Create_File_In_Saves_Folder()
        {
            // Arrange
            DeleteFilesFromSavesFolder();
            var startPos = new PosXY(0, 0);
            var level = new Level(new PosXY(20, 20), Enumerable.Empty<PosXY>());
            var gameService = GameService();
            gameService.CreateGame(startPos, level);
            IGameSaveLoader gameSaveLoader = new JsonGameSaveLoader(gameService);

            // Act
            gameSaveLoader.SaveGame();

            // Assert
            gameSaveLoader.GetSaveFiles().Count().ShouldBe(1);
        }

        [Fact]
        public void Load_Method_Should_Load_Saved_Data()
        {
            // Arrange
            DeleteFilesFromSavesFolder();
            var startPos = new PosXY(0, 0);
            var level = new Level(new PosXY(20, 20), Enumerable.Empty<PosXY>());
            var gameService = GameService();
            gameService.CreateGame(startPos, level);
            IGameSaveLoader gameSaveLoader = new JsonGameSaveLoader(gameService);

            // Act
            gameSaveLoader.SaveGame();
            var emptyGameService = GameService();
            var gameSaveLoaderForEmptyGameService = new JsonGameSaveLoader(emptyGameService);
            var fileName = gameSaveLoaderForEmptyGameService.GetSaveFiles().First();
            gameSaveLoaderForEmptyGameService.LoadGame(fileName);

            // Assert
            fileName.ShouldBe("000-000-000.json");
            emptyGameService.GetLevel().GameSize.ShouldBe(level.GameSize);
            emptyGameService.GetLevel().Walls.Count().ShouldBe(0);
            emptyGameService.GetSnakeBody().Head.ShouldBe(startPos);
        }

        #region ARRANGE
        private readonly string SaveDataFolder = Path.Combine(".", "Saves");
        private ISnakeGameService GameService()
            => new SnakeGameService(new SnakeGameManagerFactory(), new SnakeGameObjectFactory());
        private void DeleteFilesFromSavesFolder()
        {
            var files = Directory.GetFiles(SaveDataFolder);
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                fileInfo.Delete();
            }
        }

        public JsonGameSaveLoaderTests()
        {
            var directoryExist = Directory.Exists(SaveDataFolder);

            if (directoryExist is false)
            {
                Directory.CreateDirectory(SaveDataFolder);
            }
        }
        #endregion
    }
}
