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

        private void SwitchScene(Type type)
        {
            SelectedScene().UnSelect();
            Thread.Sleep(201);
            Scenes[type].Select();
            var t = Task.Run(() => SelectedScene()?.StartScene());

            if (type == typeof(Exit))
            {
                var exitScene = SelectedScene() as Exit;
                exitScene.ExitFromApp += () => exit = true;
            }
        }
    }
}
