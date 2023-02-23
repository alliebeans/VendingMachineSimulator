using System;
using UI;
using UIExtensions;
using Simulator = VendingMachineSimulator;


namespace Pages
{
    public class InFrontOfVendingMachinePage : Page, Simulator::IPage
    {
        private UIAscii Ascii = new UIAscii(@"UI/ASCII/InFrontOfVendingMachinePage.txt");
        private VerticalCenterMenu vendingMachineLink = new VerticalCenterMenu(UI.Links.LinkClass.VerticalCenter, "use");
        public InFrontOfVendingMachinePage(Simulator::User user, IActionBar actionBar, Simulator::IInputReader navigationInput, UIDecorator uiDecorator) : base(user, actionBar, navigationInput, uiDecorator) 
        {
            vendingMachineLink.Select(0);
            Command = Simulator::Command.Navigate;
        }

        public override void Print()
        {
            uiDecorator.Print(Ascii);
            uiDecorator.Print(vendingMachineLink);
            user.StatusBar.Print();
            actionBar.Print();
            Read();
        }

        private void Read()
        {
            navigationInput.Read();
            var input = navigationInput.GetCurrentKey();

            if (actionBar.GetActionInput().Parse(input) != null)
                Command = (Simulator::Command)actionBar.GetActionInput().Parse(input)!;
            else if (navigationInput.IsChoiceKey(input))    
                Choose();
            else if (navigationInput.IsIterationKey(input))
            {
                vendingMachineLink.SelectState(input);
                Console.Clear();
                Print();
            }
            else
            {
                Console.Clear();
                Print();
            }
        }

        private void Choose()
        {
            var nextPage = vendingMachineLink.Chosen switch
            {
                0 => Simulator::Page.MenuPage_1,
                _ => throw new IOException(),
            };

            Command = Simulator::Command.Navigate;
            NextPage = nextPage;
        }
    }
}