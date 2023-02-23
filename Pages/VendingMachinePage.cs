using System;
using UI;
using UIExtensions;
using Simulator = VendingMachineSimulator;

namespace Pages
{
    public abstract class VendingMachinePage : Page, Simulator::IPage
    {
        protected static int heightPad = UIExtension.Height - 4;
        protected static int initialHeightOffset = 2;
        protected Simulator::VendingMachine VendingMachine;
        protected VendingMachinePage(Simulator.User user, IActionBar actionBar, Simulator.IInputReader navigationInput, UIDecorator uiDecorator, Simulator::VendingMachine vendingMachine) : base(user, actionBar, navigationInput, uiDecorator)
        {
            VendingMachine = vendingMachine;
        }

        protected void PrintTop() 
        {
            UIExtension.HeightPad(initialHeightOffset);
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            string _logo = $"BeansVendor3000™";
            string _inCart = $"In cart: ({VendingMachine.ProductsInCart.Count()})";
            string _balance = $"Balance: {VendingMachine.CashBalance.ToString("C", UIExtension.SE())}";
            Console.Write($"{_logo}{_inCart.PadLeft(UIExtension.VendingMachineWidth - _logo.Length - 4)}");
            Console.ResetColor();
            Console.WriteLine();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            Console.WriteLine(_balance);
            return;
        }
    }
}