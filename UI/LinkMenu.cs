using System;

namespace UI
{
    public abstract class LinkMenu : IUIComponent
    {
        protected Links.LinkClass linkClass { get; set; }
        public Link[] Menu { get; private set; }
        protected int iterator = -1;
        public int Chosen => iterator;

        public LinkMenu(Links.LinkClass linkClass, string name)
        {
            this.linkClass = linkClass;

            Menu = new Link[1];

            Menu[0] = linkClass switch 
            {
                Links.LinkClass.Back => new Links.ArrowLink(Links.LinkClass.Back, name),
                Links.LinkClass.Next => new Links.ArrowLink(Links.LinkClass.Next, name),
                Links.LinkClass.VerticalCenter => new Links.IterationLink(name),
                Links.LinkClass.HorizontalCenter => new Links.IterationLink(name),
                Links.LinkClass.Grid => new Links.GridLink(name),
                _ => throw new IOException(),
            };
        }

        public LinkMenu(Links.LinkClass linkClass, string name0, string name1)
        {
            if (linkClass == Links.LinkClass.BackNext)
            {
                this.linkClass = linkClass;

                Menu = new Link[2];

                Menu[0] = new Links.ArrowLink(Links.LinkClass.Back, name0);
                Menu[1] = new Links.ArrowLink(Links.LinkClass.Next, name1);
            }
            else
            {
                var names = new[] {name0, name1};

                Menu = new Link[2];

                for (int i = 0; i < Menu.Length; i++)
                {
                    Menu[i] = linkClass switch 
                    {
                        Links.LinkClass.VerticalCenter => new Links.IterationLink(names[i]),
                        Links.LinkClass.HorizontalCenter => new Links.IterationLink(names[i]),
                        Links.LinkClass.HorizontalLeftRight => new Links.IterationLink(names[i]),
                        Links.LinkClass.CenterLeftRight => new Links.IterationLink(names[i]),
                        Links.LinkClass.Grid => new Links.GridLink(names[i]),
                        _ => throw new IOException(),
                    };
                }
            }     
        }

        public LinkMenu(Links.LinkClass linkClass, params string[] names)
        {
            this.linkClass = linkClass;
            Menu = new Link[names.Length];

            for (int i = 0; i < Menu.Length; i++)
            {
                Menu[i] = linkClass switch 
                {
                    Links.LinkClass.VerticalCenter => new Links.IterationLink(names[i]),
                    Links.LinkClass.HorizontalCenter => new Links.IterationLink(names[i]),
                    Links.LinkClass.Grid => new Links.GridLink(names[i]),
                    _ => throw new IOException(),
                };
            }
        }

        public void Select(int select)
        {
            if (select == -1) 
                foreach(Link link in Menu)
                    link.IsSelected = false;
            else
                Menu[select].IsSelected = true;
            
            iterator = select;
        }

        public void SelectState(ConsoleKey key)
        {
            foreach(Link link in Menu)
                link.IsSelected = false;
            
            switch (key)
            {
                case ConsoleKey.RightArrow:
                case ConsoleKey.DownArrow:
                if (iterator < Menu.Length)
                    iterator++;
                if (iterator == Menu.Length)
                    iterator = 0;
                break;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.UpArrow:
                if (iterator >= 0)
                    iterator--;
                if (iterator < 0)
                    iterator = Menu.Length-1;
                break;
            }

            Select(iterator);
            return;
        }

        public abstract void Print();
    }
}