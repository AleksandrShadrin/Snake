using Snake.Presentation.Selectable;

namespace Snake.Presentation.Base
{
    public class BaseSelectable : ISelectable
    {
        public bool Selected { get; private set; } = false;

        public void Select()
        {
            Selected = true;
        }

        public void UnSelect()
        {
            Selected = false;
        }
    }
}
