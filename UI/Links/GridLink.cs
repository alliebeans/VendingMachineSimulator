using System;
using UIExtensions;

namespace UI.Links
{
    public class GridLink : Link, IUIComponent
    {
        public new string PrintName = String.Empty;
        public GridLink(string name) : base(name) {}

        public override void Print()
        {
            PrintName = $"<{this.Name}>";
            if (!IsSelected)
                Console.Write($"{PrintName}");
            else
                Console.Write($"{PrintName}", UIExtensions.UIExtension.InvertColor());
                Console.ResetColor();
        }
    }
}