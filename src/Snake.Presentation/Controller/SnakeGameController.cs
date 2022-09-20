using Snake.Application.Adapters;
using Snake.Presentation.Base;
using Snake.Presentation.LevelGenerator;

namespace Snake.Presentation.Controller
{
    public class SnakeGameController : IController
    {
        private readonly ISnakeGameService gameService;
        private readonly IGameSaveLoader gameSaveLoader;
        private readonly InputHandler inputHandler;

        private bool exit = false;
        private Dictionary<string, BaseScene> Scenes = new();

        private BaseScene SelectedScene()
            => Scenes
                .Where(kvp => kvp.Value.Selected is true)
                .Select(kvp => kvp.Value)
                .FirstOrDefault();

        public SnakeGameController(ISnakeGameService gameService, IGameSaveLoader gameSaveLoader, InputHandler inputHandler, ILevelGenerator levelGenerator)
        {
            this.gameService = gameService;
            this.inputHandler = inputHandler;
            this.gameSaveLoader = gameSaveLoader;

            gameService.CreateGame(levelGenerator.GenerateLevel().Item1, levelGenerator.GenerateLevel().Item2);
        }

        public void AddSceneToController(string sceneName, BaseScene scene)
        {
            scene.OnSwitchScene += SwitchScene;
            inputHandler.OnChange += scene.OnKeyPressed;
            Scenes.Add(sceneName.ToLower(), scene);
        }

        public Task Start()
        {
            Task.Run(() => SelectedScene()?.StartScene());

            while (exit is false)
            {
                Thread.Sleep(200);
            }

            SelectedScene()?.UnSelect();
            Console.Clear();

            return Task.CompletedTask;
        }

        private void SwitchScene(string sceneName)
        {
            if (sceneName == "exit")
            {
                exit = true;
                return;
            }

            SelectedScene().UnSelect();
            Scenes[sceneName.ToLower()].Select();
            Task.Run(() => SelectedScene()?.StartScene());
        }
    }
}
