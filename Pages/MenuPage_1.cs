using System;
using UI;
using UIExtensions;
using Products = Products;
using Simulator = VendingMachineSimulator;

namespace Pages
{
    public class MenuPage_1 : VendingMachinePage, Simulator::IPage
    {
        protected VerticalCenterMenu categoriesMenu;
        protected NextMenu checkoutLink;
        public MenuPage_1(Simulator::User user, IActionBar actionBar, Simulator::IInputReader navigationInput, UIDecorator uiDecorator, Simulator::VendingMachine vendingMachine) : base(user, actionBar, navigationInput, uiDecorator, vendingMachine) 
        {
            string[] categoriesNames = new HashSet<string>(vendingMachine.AvailableProducts.Select(product => product.GetCategoryName())).ToArray();
            categoriesMenu = new VerticalCenterMenu(UI.Links.LinkClass.VerticalCenter, categoriesNames);
            categoriesMenu.Select(0);
            heightPad -= initialHeightOffset + categoriesMenu.Menu.Count();

            checkoutLink = new NextMenu(UI.Links.LinkClass.Next, "Checkout");
            checkoutLink.Select(-1);
            
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
            /* else
                UIExtension.HeightPad((heightPad) -4); */

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
                VendingMachine.SetActivePage(Simulator.Page.MenuPage_1);
                Command = (Simulator::Command)actionBar.GetActionInput().Parse(input)!;
            }    
            else if (navigationInput.IsNavigationKey(input))
            {
                
                if (input == ConsoleKey.Escape)
                {
                    if (VendingMachine.CashBalance > 0)
                    {
                        user.Wallet.CashInWallet.AddRange(VendingMachine.GetChangeFromVendingMachine(VendingMachine.CashBalance));
                        VendingMachine.CashInMachine.Clear();
                    }
                    Command = Simulator::Command.Navigate;
                    NextPage = Simulator::Page.InFrontOfVendingMachinePage;
                } 
                else //if (VendingMachine.ProductsInCart.Count() > 0  || VendingMachine.CashBalance > 0)
                {
                    Command = Simulator::Command.Navigate;
                    NextPage = Simulator::Page.MenuPage_2;
                }
            }
            else if (navigationInput.IsIterationKey(input))
            {
                if (categoriesMenu.Chosen != -1)
                {
                    categoriesMenu.SelectState(input);
                    Console.Clear();
                    Print();
                }
            }
            else if (navigationInput.IsChoiceKey(input))    
            {
                Choose();
            }
            else 
            {
                Console.Clear();
                Print();
            }
        }

        private void Choose()
        {
            var _activeCategory = categoriesMenu.Chosen switch
            {
                0 => Simulator::ProductCategory.Drinks,
                1 => Simulator::ProductCategory.Candy,
                2 => Simulator::ProductCategory.Snacks,
                3 => Simulator::ProductCategory.Pastries,
                4 => Simulator::ProductCategory.Healthy,
                _ => throw new IOException(),
            };

            VendingMachine.SetActiveCategory(_activeCategory);
        
            NextPage = Simulator::Page.SubMenuPage_1;
            Command = Simulator::Command.Navigate;
        }
    }
}