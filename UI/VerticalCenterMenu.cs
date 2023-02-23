using System;
using UIExtensions;
using UI.Links;

namespace UI
{
    public class VerticalCenterMenu : LinkMenu, IUIComponent
    {
        public VerticalCenterMenu(LinkClass linkClass, string name) : base(linkClass, name) {}

        public VerticalCenterMenu(LinkClass linkClass, params string[] names) : base(linkClass, names) {}

        public VerticalCenterMenu(LinkClass linkClass, string name0, string name1) : base(linkClass, name0, name1) {}

        public override void Print()
        {
            for (int i = 0; i < Menu.Length; i++)
            {
                Console.Write("".PadLeft((UIExtension.Width / 2) - (Menu[i].PrintName.Length / 2)));
                Menu[i].Print();
                Console.WriteLine();
            }
        }
    }
}