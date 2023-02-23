using System;
using UIExtensions;
using UI.Links;

namespace UI
{
    public class NextMenu : LinkMenu, IUIComponent
    {
        public NextMenu(LinkClass linkClass, string name) : base(linkClass, name) {}

        public NextMenu(LinkClass linkClass, params string[] names) : base(linkClass, names) {}

        public NextMenu(LinkClass linkClass, string name0, string name1) : base(linkClass, name0, name1) {}

        public override void Print()
        {
            Console.Write("".PadLeft(UIExtension.VendingMachineWidth - (Menu[0].PrintName.Length)));
            Menu[0].Print();
            Console.WriteLine();
        }
    }
}