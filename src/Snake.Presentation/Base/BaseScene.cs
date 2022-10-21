using Snake.Presentation.Renderable;

namespace Snake.Presentation.Base
{
    public abstract class BaseScene : BaseSelectable, IRenderable
    {
        /// <summary>
        /// Запуск сцены
        /// </summary>
        public abstract Task StartScene();

        /// <summary>
        /// Метод выполняется, при нажатии на клавишу
        /// </summary>
        public abstract void DoOnKeyPressed();

        public abstract void Render();

        /// <summary>
        /// Переключить сцену
        /// </summary>
        public Action<Type> SwitchScene { get; set; }

        /// <summary>
        /// Выполняет DoOnKeyPressed, когда сцена выбрана
        /// </summary>
        public void OnKeyPressed()
        {
            if (Selected)
                DoOnKeyPressed();
        }
    }
}
