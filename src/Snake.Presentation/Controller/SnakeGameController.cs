using Snake.Application.Adapters;
using Snake.Presentation.Base;
using Snake.Presentation.LevelGenerator;
using Snake.Presentation.Scenes;

namespace Snake.Presentation.Controller
{
    public class SnakeGameController : IController
    {
        private readonly ISnakeGameService gameService;
        private readonly InputHandler inputHandler;

        private bool exit = false;
        private Task launchedTask;
        private Dictionary<Type, BaseScene> Scenes = new();

        private BaseScene SelectedScene()
            => Scenes
                .Where(kvp => kvp.Value.Selected is true)
                .Select(kvp => kvp.Value)
                .FirstOrDefault();

        public SnakeGameController(ISnakeGameService gameService, IGameSaveLoader gameSaveLoader, InputHandler inputHandler, ILevelGenerator levelGenerator)
        {
            this.gameService = gameService;
            this.inputHandler = inputHandler;

            gameService.CreateGame(levelGenerator.GenerateLevel().Item1, levelGenerator.GenerateLevel().Item2);
        }

        public void AddSceneToController(BaseScene scene)
        {
            scene.OnSwitchScene += SwitchScene;
            inputHandler.OnChange += scene.OnKeyPressed;
            Scenes.Add(scene.GetType(), scene);
        }

        public async Task Start()
        {
            launchedTask = Task.Run(() => SelectedScene()?.StartScene());
            
            while (exit is false)
            {
                await Task.Delay(200);

            }

            Console.Clear();
        }

        private void SwitchScene(Type type)
        {
            SelectedScene().UnSelect();
            Scenes[type].Select();

            launchedTask.Wait();
            inputHandler.ClearConsoleKeyInfo();
            launchedTask = SelectedScene()?.StartScene();

            if (type == typeof(Exit))
            {
                var exitScene = SelectedScene() as Exit;
                exitScene.ExitFromApp += () => exit = true;
            }
        }
    }
}
