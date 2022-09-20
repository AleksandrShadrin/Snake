using Microsoft.Extensions.DependencyInjection;
using Snake.Application.Adapters;

namespace Snake.Application
{
    public static class Extensions
    {
        public static IServiceCollection RegisterApplication(this IServiceCollection services)
        {
            services.AddSingleton<IGameSaveLoader, JsonGameSaveLoader>();
            services.AddSingleton<ISnakeGameService, SnakeGameService>();
            return services;
        }
    }
}
