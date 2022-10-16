using Microsoft.Extensions.DependencyInjection;
using Snake.Presentation.Controller;
using Snake.Presentation.Scenes;

namespace Snake.Presentation;

public class App
{
    private readonly IServiceProvider _serviceProvider;

    public App(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Run()
    {
        var controller = _serviceProvider.GetRequiredService<IController>();
        RegisterScenes(controller);

        await controller.Start();
    }

    private void RegisterScenes(IController controller)
    {
        var snakeMenu = _serviceProvider.GetService<SnakeMenu>();
        var snakeGame = _serviceProvider.GetService<SnakeGame>();
        var saveScene = _serviceProvider.GetService<SaveScene>();
        var loadScene = _serviceProvider.GetService<LoadScene>();
        var exit = _serviceProvider.GetService<Exit>();

        controller.AddSceneToController(snakeMenu);
        controller.AddSceneToController(snakeGame);
        controller.AddSceneToController(saveScene);
        controller.AddSceneToController(loadScene);
        controller.AddSceneToController(exit);

        snakeMenu.Select();
    }
}