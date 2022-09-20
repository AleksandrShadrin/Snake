using Microsoft.Extensions.DependencyInjection;

namespace Snake.Core.Factories
{
    public static class Extensions
    {
        public static IServiceCollection RegisterFactories(this IServiceCollection services)
        {
            services.AddSingleton<ISnakeGameManagerFactory, SnakeGameManagerFactory>();
            services.AddSingleton<ISnakeGameObjectFactory, SnakeGameObjectFactory>();
            return services;
        }
    }
}
