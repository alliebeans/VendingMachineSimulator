using System;
using VendingMachineSimulator;
using Simulator = VendingMachineSimulator;
using UI = UI;

namespace Pages
{
    public abstract class Page : Simulator::IPage
    {
        protected Simulator::User user;
        protected UI::IActionBar actionBar;
        protected Simulator::IInputReader navigationInput;
        protected UI::IStatusBar statusBar;
        protected UI::UIDecorator uiDecorator;
        protected Simulator::Page NextPage;
        protected Simulator::Command Command;
        public Simulator::Page GetNextPage() => NextPage;
        public Simulator::Command GetCommand() => Command;

        public Page(Simulator::User user, UI::IActionBar actionBar, Simulator::IInputReader navigationInput, UI::UIDecorator uiDecorator) 
        {
            this.user = user;
            statusBar = user.StatusBar;
            this.actionBar = actionBar;
            this.uiDecorator = uiDecorator;
            this.navigationInput = navigationInput;
        }

        public abstract void Print();
    }
}