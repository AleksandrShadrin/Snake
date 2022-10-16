using Microsoft.Extensions.DependencyInjection;
using Snake.Application;
using Snake.Core.Factories;
using Snake.Presentation.Controller;
using Snake.Presentation.LevelGenerator;
using Snake.Presentation.Scenes;
using Snake.Presentation.Services;

namespace Snake.Presentation;

public static class RegisterServices
{
    public static IServiceCollection Register(this IServiceCollection services)
    {
        services.RegisterFactories();
        services.RegisterApplication();
        services.AddSingleton<IGameSnakeRenderService, ConsoleGameSnakeRenderService>();
        services.AddSingleton<IController, SnakeGameController>();
        services.AddSingleton<SnakeGame>();
        services.AddSingleton<SnakeMenu>();
        services.AddSingleton<SaveScene>();
        services.AddSingleton<LoadScene>();
        services.AddSingleton<Exit>();
        services.AddSingleton<ILevelGenerator, DefaultLevelGenerator>();
        services.AddSingleton(typeof(InputHandler), (s) =>
        {
            var ih = new InputHandler();
            ih.StartHandleConsoleInput();
            return ih;
        });
        services.AddSingleton<App>();
        return services;
    }
}