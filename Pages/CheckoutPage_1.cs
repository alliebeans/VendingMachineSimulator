using System;
using UI;
using UIExtensions;
using VendingMachineSimulator;
using Simulator = VendingMachineSimulator;

namespace Pages
{
    public class CheckoutPage_1 : VendingMachinePage, Simulator::IPage
    {
        protected CenterLeftRightMenu purchaseConfirmation;
        public CheckoutPage_1(User user, IActionBar actionBar, IInputReader navigationInput, UIDecorator uiDecorator, VendingMachine vendingMachine) : base(user, actionBar, navigationInput, uiDecorator, vendingMachine)
        {
            purchaseConfirmation = new CenterLeftRightMenu(UI.Links.LinkClass.CenterLeftRight, "⟳ Start over", "Confirm purchase");
            purchaseConfirmation.Select(1);
        }

        public override void Print()
        {
            VendingMachine.SetActivePage(Simulator.Page.CheckoutPage_1);
            UIExtension.HeightPad(initialHeightOffset);
            base.PrintTop();
            
            UIExtension.HeightPad(heightPad / 3);
            var title = "YOUR CART";
            var item = "Item";
            var subTotal = "Subtotal";
            var cartPad = item.Length+32+subTotal.Length;

            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            Console.WriteLine(title);
            Console.WriteLine();

            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            Console.Write($"{item}".PadRight(32));
            Console.Write(subTotal);
            Console.WriteLine();

            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            Console.WriteLine("".PadRight(cartPad, '='));
            foreach(Products.Product product in VendingMachine.ProductsInCart.Take(3))
            {
                UIExtension.LeftScreenPad();
                UIExtension.LeftScreenPad();
                UIExtension.LeftScreenPad();
                UIExtension.LeftScreenPad();
                Console.Write($"{product.GetName()}{product.Price.ToString("C", UIExtension.SE()).PadLeft(cartPad-product.GetName().Length)}");
                Console.WriteLine();
            }
            if (VendingMachine.ProductsInCart.Count() > 3)
            {
                UIExtension.LeftScreenPad();
                UIExtension.LeftScreenPad();
                UIExtension.LeftScreenPad();
                UIExtension.LeftScreenPad();
                Console.WriteLine($"... and ({VendingMachine.ProductsInCart.Count() - 3}) more...");
            }
            else 
            {
                var emtpyProductSpace = (4 - VendingMachine.ProductsInCart.Take(4).Count()); 
                for (int i = 0; i < emtpyProductSpace;i++)
                    Console.WriteLine();
            }
            
            Console.WriteLine();

            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            Console.Write("".PadRight(cartPad));
            var balance = "Balance:";
            var total = "Total:";
            Console.Write($"{balance}{VendingMachine.CashBalance.ToString("C", UIExtension.SE()).PadLeft(22-balance.Length)}");
            Console.WriteLine();

            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            Console.Write("".PadRight(cartPad));
            if (VendingMachine.CashTotal() > 0)
            {
                Console.Write($"{total}{VendingMachine.CashTotal().ToString("C", UIExtension.SE()).PadLeft(22-total.Length)}");
            }
            else 
            {
                Console.Write($"{total}{VendingMachine.CashTotal().ToString("C", UIExtension.SE()).PadLeft(22-total.Length)}");
            }
            Console.WriteLine();

            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            Console.Write("".PadRight(cartPad));
            Console.WriteLine("".PadRight(22, '-'));

            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            Console.Write("".PadRight(cartPad));
            if (VendingMachine.PurchaseTotalToPay() >= 0)
            {
                Console.Write($"To Pay: {VendingMachine.PurchaseTotalToPay().ToString("C", UIExtension.SE()).PadLeft(22-total.Length)}");
            }
            else 
            {
                Console.Write($"To Pay: {VendingMachine.PurchaseTotalToPay().ToString("C", UIExtension.SE()).PadLeft(22-total.Length)}");
            }

            Console.WriteLine();
            Console.WriteLine();

            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            uiDecorator.Print(purchaseConfirmation);
            
            user.StatusBar.Print();
            actionBar.Print();

            Read();
        }

        private void Read()
        {
            navigationInput.Read();
            var input = navigationInput.GetCurrentKey();
            
            if (actionBar.GetActionInput().Parse(input) != null)
            {
                VendingMachine.SetActivePage(Simulator.Page.CheckoutPage_1);
                Command = (Simulator::Command)actionBar.GetActionInput().Parse(input)!;
            }    
            else if (navigationInput.IsNavigationKey(input))
            {
                Command = Simulator.Command.Navigate;
                NextPage = Simulator.Page.MenuPage_1;
            }
            else if (navigationInput.IsChoiceKey(input))
                Choose();
            else if (navigationInput.IsIterationKey(input))
            {
                purchaseConfirmation.SelectState(input);
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
            if (purchaseConfirmation.Chosen == 1)
            {
                if (VendingMachine.PurchaseTotalToPay() <= 0)
                {
                    if (VendingMachine.PurchaseTotalToPay() < 0)
                    {
                        user.Inventory.GetContent().AddRange(VendingMachine.ProductsInCart);
                        var cashBack = (VendingMachine.PurchaseTotalToPay()) * -1;
                        user.Wallet.CashInWallet.AddRange(VendingMachine.GetChangeFromVendingMachine(cashBack));
                        VendingMachine.CashInMachine.Clear();
                        VendingMachine.ProductsInCart.Clear();

                        Command = Simulator.Command.Navigate;
                        NextPage = Simulator.Page.InFrontOfVendingMachinePage;
                    }
                    else 
                    {
                        user.Inventory.GetContent().AddRange(VendingMachine.ProductsInCart);
                        VendingMachine.ProductsInCart.Clear();
                        VendingMachine.CashInMachine.Clear();
                        
                        Command = Simulator.Command.Navigate;
                        NextPage = Simulator.Page.InFrontOfVendingMachinePage;
                    }
                }
                else 
                {
                    actionBar.Message("You realize you need to insert more cash for this to work...");
                    Console.Clear();
                    Print();
                }
            }
            else if (purchaseConfirmation.Chosen == 0)
            {
                VendingMachine.ProductsInCart.Clear();
                Command = Simulator.Command.Navigate;
                NextPage = Simulator.Page.MenuPage_1; 
            }
        }
    }
}