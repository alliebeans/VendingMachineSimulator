using System;
using UIExtensions;

namespace UI.Links
{
    public class ArrowLink : Link, IUIComponent
    {
        private LinkClass nextDirection;
        public new string PrintName = String.Empty;
        public ArrowLink(LinkClass nextDirection, string name) : base(name) 
        {
            this.nextDirection = nextDirection;
        }

        public override void Print()
        {
            switch (nextDirection)
            {
                case LinkClass.Next:
                PrintName = $"{this.Name} 🠚";
                    if (!IsSelected)
                        Console.Write($"{PrintName}");
                    else
                        Console.Write($"{PrintName}", UIExtensions.UIExtension.InvertColor());
                        Console.ResetColor();
                return;
                case LinkClass.Back:
                PrintName = $"🠘 {this.Name}";
                    if (!IsSelected)
                        Console.Write($"{PrintName}");
                    else
                        Console.Write($"{PrintName}", UIExtensions.UIExtension.InvertColor());
                        Console.ResetColor();
                return;
            }
        }
    }
}