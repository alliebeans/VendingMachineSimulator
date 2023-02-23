using System;
using UIExtensions;
using UI.Links;

namespace UI
{
    public class GridMenu : LinkMenu, IUIComponent
    {
        public GridMenu(LinkClass linkClass, string name) : base(linkClass, name) {}

        public GridMenu(LinkClass linkClass, params string[] names) : base(linkClass, names) {}

        public GridMenu(LinkClass linkClass, string name0, string name1) : base(linkClass, name0, name1) {}

        public override void Print() { throw new InvalidOperationException(); }

        public void Print(int iterator)
        {
            UIExtension.ProductPad();
            Menu[iterator].Print();
        }
    }
}