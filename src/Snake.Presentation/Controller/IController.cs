using Snake.Presentation.Base;

namespace Snake.Presentation.Controller
{
    internal interface IController
    {
        void AddSceneToController(string sceneName, BaseScene scene);
        Task Start();
    }
}
