using Microsoft.Extensions.DependencyInjection;
using Snake.Presentation;
using Snake.Presentation.Controller;
using Snake.Presentation.Scenes;

IServiceCollection services = new ServiceCollection()
    .Register();

var provider = services.BuildServiceProvider();

var app = provider.GetService<App>();
await app.Run();
