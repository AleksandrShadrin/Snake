using Snake.Presentation.Renderable;

namespace Snake.Presentation.Base
{
    public abstract class BaseScene : BaseSelectable, IRenderable
    {
        public abstract void StartScene();
        public abstract void DoOnKeyPressed();
        public abstract void Render();
        public Action<string> OnSwitchScene { get; set; }


        public void OnKeyPressed()
        {
            if(Selected is false)
                return;

            DoOnKeyPressed();
        }
    }
}
