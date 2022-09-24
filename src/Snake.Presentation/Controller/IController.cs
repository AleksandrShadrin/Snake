using Snake.Presentation.Base;

namespace Snake.Presentation.Controller
{
    public interface IController
    {
        void AddSceneToController(BaseScene scene);
        Task Stop();
        Task Start();
    }
}
