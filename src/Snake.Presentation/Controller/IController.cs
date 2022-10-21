using Snake.Presentation.Base;

namespace Snake.Presentation.Controller
{
    public interface IController
    {
        /// <summary>
        /// Добавляет сцену к контроллеру
        /// </summary>
        /// <param name="scene">BaseScene, который необходимо добавить</param>
        void AddSceneToController(BaseScene scene);

        /// <summary>
        /// Останавливает работу контроллера
        /// </summary>
        Task Stop();

        /// <summary>
        /// Запускает контроллер
        /// </summary>
        Task Start();
    }
}
