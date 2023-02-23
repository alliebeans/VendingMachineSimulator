using System;
using UI;
using UIExtensions;
using Products = Products;
using Simulator = VendingMachineSimulator;

namespace Pages
{
    public class MenuPage_2 : VendingMachinePage, Simulator::IPage
    {
        protected VerticalCenterMenu categoriesMenu;
        protected NextMenu checkoutLink;
        public MenuPage_2(Simulator::User user, IActionBar actionBar, Simulator::IInputReader navigationInput, UIDecorator uiDecorator, Simulator::VendingMachine vendingMachine) : base(user, actionBar, navigationInput, uiDecorator, vendingMachine) 
        {
            string[] categoriesNames = new HashSet<string>(vendingMachine.AvailableProducts.Select(product => product.GetCategoryName())).ToArray();
            categoriesMenu = new VerticalCenterMenu(UI.Links.LinkClass.VerticalCenter, categoriesNames);
            categoriesMenu.Select(-1);
            heightPad -= initialHeightOffset + categoriesMenu.Menu.Count();

            checkoutLink = new NextMenu(UI.Links.LinkClass.Next, "Checkout");
            checkoutLink.Select(0);
            
            Command = Simulator.Command.Navigate;
        }

        public override void Print()
        {
            UIExtension.HeightPad(initialHeightOffset);
            base.PrintTop();

            UIExtension.HeightPad(heightPad / 2);
            uiDecorator.Print(categoriesMenu);
            UIExtension.HeightPad(heightPad / 4);

            //if(VendingMachine.ProductsInCart.Count() > 0 || VendingMachine.CashBalance > 0)
            //{
                uiDecorator.Print(checkoutLink);
                UIExtension.HeightPad((heightPad) -5);
            //}
            //else
                //UIExtension.HeightPad((heightPad) -4);

            user.StatusBar.Print();
            actionBar.Print();

            Thread.Sleep(100);
            NextPage = Simulator::Page.CheckoutPage_1;
        }

        private void Read()
        {
            navigationInput.Read();
            var input = navigationInput.GetCurrentKey();    

            if (actionBar.GetActionInput().Parse(input) != null)
            {
                VendingMachine.SetActivePage(Simulator.Page.MenuPage_2);
                Command = (Simulator::Command)actionBar.GetActionInput().Parse(input)!;
            }
            else if (navigationInput.IsNavigationKey(input))
            {
                if (input == ConsoleKey.Escape)
                {
                    Command = Simulator::Command.Navigate;
                    NextPage = Simulator::Page.InFrontOfVendingMachinePage;
                } 
                
                else //if (VendingMachine.ProductsInCart.Count() > 0  || VendingMachine.CashBalance > 0)
                {
                    Command = Simulator::Command.Navigate;
                    NextPage = Simulator::Page.MenuPage_1;
                }
            }
            else if (navigationInput.IsIterationKey(input))
            {
                if (checkoutLink.Chosen != -1)
                {
                    checkoutLink.SelectState(input);
                    Console.Clear();
                    Print();
                }
            }
            else if (navigationInput.IsChoiceKey(input))    
            {
                Simulator::Page nextPage = Simulator::Page.MenuPage_1;

                if (checkoutLink.Chosen >= 0) 
                {
                    nextPage = checkoutLink.Chosen switch
                    {
                        0 => Simulator::Page.CheckoutPage_1,
                        _ => throw new IOException(),
                    };
                }

                Command = Simulator::Command.Navigate;
                NextPage = nextPage;
            }
            else 
            {
                Console.Clear();
                Print();
            }
        }
    }
}