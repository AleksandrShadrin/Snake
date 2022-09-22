using Snake.Presentation.Base;

namespace Snake.Presentation.Controller
{
    internal interface IController
    {
        void AddSceneToController(BaseScene scene);
        Task Start();
    }
}
