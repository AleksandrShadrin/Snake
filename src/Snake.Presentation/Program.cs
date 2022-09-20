using Microsoft.Extensions.DependencyInjection;
using Snake.Application;
using Snake.Core.Factories;
using Snake.Presentation;
using Snake.Presentation.Controller;
using Snake.Presentation.LevelGenerator;
using Snake.Presentation.Scenes;
using Snake.Presentation.Services;

IServiceCollection services = new ServiceCollection();
services.RegisterFactories();
services.RegisterApplication();
services.AddSingleton<IGameSnakeRenderService, ConsoleGameSnakeRenderService>();
services.AddSingleton<IController, SnakeGameController>();
services.AddSingleton<SnakeGame>();
services.AddSingleton<SnakeMenu>();
services.AddSingleton<SaveScene>();
services.AddSingleton<LoadScene>();
services.AddSingleton<ILevelGenerator, DefaultLevelGenerator>();
services.AddSingleton(typeof(InputHandler), (s) =>
{
    var ih = new InputHandler();
    ih.StartHandleConsoleInput();
    return ih;
});

var provider = services.BuildServiceProvider();
var snakeMenu = provider.GetService<SnakeMenu>();
var snakeGame = provider.GetService<SnakeGame>();
var saveScene = provider.GetService<SaveScene>();
var loadScene = provider.GetService<LoadScene>();

var controller = provider.GetService<IController>();
controller.AddSceneToController(nameof(snakeMenu), snakeMenu);
controller.AddSceneToController(nameof(snakeGame), snakeGame);
controller.AddSceneToController(nameof(saveScene), saveScene);
controller.AddSceneToController(nameof(loadScene), loadScene);

snakeMenu.Select();
await controller.Start();
Console.Clear();
