using System;
using UIExtensions;
using UI.Links;

namespace UI
{
    public class CenterLeftRightMenu : LinkMenu, IUIComponent
    {
        public CenterLeftRightMenu(LinkClass linkClass, string name) : base(linkClass, name) {}

        public CenterLeftRightMenu(LinkClass linkClass, params string[] names) : base(linkClass, names) {}

        public CenterLeftRightMenu(LinkClass linkClass, string name0, string name1) : base(linkClass, name0, name1) {}

        public override void Print()
        {
            Console.Write("".PadLeft((UIExtension.Width / 2) - (Menu[0].PrintName.Length / 2) - 3 - (Menu[1].PrintName.Length)));
            Menu[0].Print();
            Console.Write("".PadLeft(3));
            Menu[1].Print();
            Console.WriteLine();
        }
    }
}