using System;

namespace UI
{
    public class TitleBar : IUIComponent
    {
        private string Title;
        public TitleBar(string title)
        {
            Title = title;
        }
        public void Print()
        {
            throw new NotImplementedException();
        }
    }
}