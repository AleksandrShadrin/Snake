﻿using Snake.Presentation.Renderable;

namespace Snake.Presentation.Base
{
    public abstract class BaseScene : BaseSelectable, IRenderable
    {
        public abstract Task StartScene();
        public abstract void DoOnKeyPressed();
        public abstract void Render();
        public Action<Type> OnSwitchScene { get; set; }


        public void OnKeyPressed()
        {
            if(Selected)
                DoOnKeyPressed();
        }
    }
}
