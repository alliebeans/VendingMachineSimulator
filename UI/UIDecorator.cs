using System;

namespace UI
{
    public class UIDecorator
    {
        public void Print(IUIComponent uiComponent) => uiComponent.Print();
    }
}