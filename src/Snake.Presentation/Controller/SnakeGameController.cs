using Snake.Application.Adapters;
using Snake.Presentation.Base;
using Snake.Presentation.LevelGenerator;

namespace Snake.Presentation.Controller
{
    public class SnakeGameController : IController
    {
        private readonly ISnakeGameService _gameService;
        private readonly InputHandler _inputHandler;

        private bool _exit = false;
        private Task _launchedTask;
        private Dictionary<Type, BaseScene> _scenes = new();

        private BaseScene? SelectedScene()
            => _scenes
                .Where(kvp => kvp.Value.Selected is true)
                .Select(kvp => kvp.Value)
                .FirstOrDefault();

        public SnakeGameController(ISnakeGameService gameService, IGameSaveLoader gameSaveLoader,
            InputHandler inputHandler, ILevelGenerator levelGenerator)
        {
            this._gameService = gameService;
            this._inputHandler = inputHandler;

            gameService.CreateGame(levelGenerator.GenerateLevel().Item1, levelGenerator.GenerateLevel().Item2);
        }

        public void AddSceneToController(BaseScene scene)
        {
            scene.OnSwitchScene += SwitchScene;
            _inputHandler.OnChange += scene.OnKeyPressed;
            _scenes.Add(scene.GetType(), scene);
        }

        public async Task Start()
        {
            _launchedTask = Task.Run(() => SelectedScene()?.StartScene());

            while (_exit is false)
            {
                await Task.Delay(200);
            }

            Console.Clear();
        }

        private void SwitchScene(Type type)
        {
            SelectedScene()?.UnSelect();

            _launchedTask.Wait();
            _scenes[type].Select();
            _inputHandler.ClearConsoleKeyInfo();
            _launchedTask = SelectedScene()?.StartScene();
        }

        public async Task Stop()
        {
            SelectedScene()?.UnSelect();
            await _launchedTask;
            _exit = true;
        }
    }
}
