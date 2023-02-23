using System;

namespace UI
{
    public abstract class Link : IUIComponent
    {
        protected string Name;
        public string GetName() => Name;
        public string PrintName = String.Empty;
        public bool IsSelected = false;

        public Link(string name)
        {
            Name = $"{name.ToUpper()}";
            PrintName = Name;
        }

        public abstract void Print();
    }
}