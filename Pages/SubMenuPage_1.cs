using System;
using UI;
using UIExtensions;
using Simulator = VendingMachineSimulator;

namespace Pages
{
    public class SubMenuPage_1 : VendingMachinePage, Simulator::IPage
    {
        private int currentMenuHeight;
        public SubMenuPage_1(Simulator::User user, IActionBar actionBar, Simulator::IInputReader navigationInput, UIDecorator uiDecorator, Simulator::VendingMachine vendingMachine) : base(user, actionBar, navigationInput, uiDecorator, vendingMachine)
        {
            VendingMachine.SetActivePage(Simulator.Page.SubMenuPage_1);
        }

        public override void Print()
        {
            GridMenu productSelection = GetProductSelection();
            
            PrintCurrentState(productSelection);
        }

        private GridMenu GetProductSelection()
        {
            var totalProductsToPrint = VendingMachine.AvailableProducts.Where(product => product.GetCategory() == VendingMachine.ActiveCategory).Count();

            string[] gridLinkNames = new string[totalProductsToPrint];
            for (int i = 0; i < totalProductsToPrint;i++)
                gridLinkNames[i] = String.Join("", "Select");
            GridMenu productSelection = new GridMenu(UI.Links.LinkClass.Grid, gridLinkNames);
            productSelection.Select(0);

            currentMenuHeight = productSelection.Menu.Length + 3;
            
            return productSelection;
        }        

        private void PrintCurrentState(GridMenu productSelection)
        {
            var totalProductsToPrint = VendingMachine.AvailableProducts.Where(product => product.GetCategory() == VendingMachine.ActiveCategory).Count();
            UIExtension.HeightPad(initialHeightOffset);
            base.PrintTop();
            Console.WriteLine();

            UIExtension.HeightPad(heightPad - (currentMenuHeight / 2) - 2);
            
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            Console.WriteLine($"{VendingMachine.ActiveCategory.ToString()}");
            Console.WriteLine();

            PrintProductSelection(productSelection);
            
            if (productSelection.Menu.Length % 3 != 0)
                UIExtension.HeightPad(heightPad - (currentMenuHeight / 2) -1);
            else
                UIExtension.HeightPad(heightPad - (currentMenuHeight / 2));
            
            user.StatusBar.Print();
            actionBar.Print();

            Read(productSelection);
        }

        private void PrintProductSelection(GridMenu productSelection)
        {
            var totalProductsToPrint = VendingMachine.AvailableProducts.Where(product => product.GetCategory() == VendingMachine.ActiveCategory).Count();

            UIExtension.ProductPad();
            
            if (totalProductsToPrint > 3)
            {
                PrintProductSelectionGrid(productSelection);
            }
            else if (totalProductsToPrint <= 3)
            {
                PrintProductSelectionRow(productSelection);
            }
                return;
        }

        private void PrintProductSelectionGrid(GridMenu productSelection)
        {
            int productCount = 0;
            var totalProductsToPrint = VendingMachine.AvailableProducts.Where(product => product.GetCategory() == VendingMachine.ActiveCategory).Count();

            for (int i = 0; i < VendingMachine.AvailableProducts.Count();i++)
            {
                if (VendingMachine.AvailableProducts[i].GetCategory() == VendingMachine.ActiveCategory)
                {
                    bool isMenuRow = productCount != 0 && productCount % 3 == 0;                            
                    if (isMenuRow)
                    {
                        Console.WriteLine(); 
                            productSelection.Print(productCount-3);
                            productSelection.Print(productCount-2);
                            productSelection.Print(productCount-1);
                        Console.WriteLine();
                        Console.WriteLine();
                        UIExtension.ProductPad();
                        PrintProduct(VendingMachine.AvailableProducts[i].GetId(), VendingMachine.AvailableProducts[i].GetName(), productCount+1);
                        productCount++;
                        continue;
                    }
                    
                    PrintProduct(VendingMachine.AvailableProducts[i].GetId(), VendingMachine.AvailableProducts[i].GetName(), productCount+1);
                    productCount++;
                }
            }
            if (productCount == totalProductsToPrint)
            {
                if (totalProductsToPrint % 3 == 0)
                {
                    var rest = 3;
                    Console.WriteLine();
                    for (int j = totalProductsToPrint-rest; j < totalProductsToPrint; j++)
                        productSelection.Print(j);
                    Console.WriteLine();
                    Console.WriteLine();
                }
                else 
                {
                    var rest = totalProductsToPrint % 3;
                    Console.WriteLine();
                    for (int j = totalProductsToPrint-rest; j < totalProductsToPrint; j++)
                        productSelection.Print(j);
                    Console.WriteLine();
                    Console.WriteLine();
                }                    
            }
        }

        private void PrintProductSelectionRow(GridMenu productSelection)
        {
            int productCount = 0;
            var totalProductsToPrint = VendingMachine.AvailableProducts.Where(product => product.GetCategory() == VendingMachine.ActiveCategory).Count();

            for (int i = 0; i < VendingMachine.AvailableProducts.Count();i++)
            if (VendingMachine.AvailableProducts[i].GetCategory() == VendingMachine.ActiveCategory)
            {                    
                PrintProduct(VendingMachine.AvailableProducts[i].GetId(), VendingMachine.AvailableProducts[i].GetName(), productCount+1);
                productCount++;

                if (productCount == totalProductsToPrint)
                {
                    Console.WriteLine();
                    for (int j = 0; j < totalProductsToPrint; j++)
                        productSelection.Print(j);
                    Console.WriteLine();
                    Console.WriteLine();
                    return;
                }
            }
        }

        private void PrintProduct(int productId, string productName, int count)
        {   
            var maxNameLength = 18;
            var productPad = 12;
            
            var isLastProductInRow = count != 0 && count % 3 == 0;
            if (isLastProductInRow)
            {
                if (productName.Length >= 14)
                    Console.Write($"{productId}. {productName.Substring(0, 11)}...");
                else
                    Console.Write($"{productId}. {productName}");
            }
            else 
            {
                if (productName.Length >= 14)
                    Console.Write($"{productId}. {productName.Substring(0, 11)}...".PadRight((maxNameLength+productPad) - (maxNameLength-productPad)));
                else
                    Console.Write($"{productId}. {productName}".PadRight((maxNameLength+productPad) - (maxNameLength-productPad)));
            }
        }

        private void Read(GridMenu productSelection)
        {
            navigationInput.Read();
            var input = navigationInput.GetCurrentKey();

            if (actionBar.GetActionInput().Parse(input) != null)
                Command = (Simulator::Command)actionBar.GetActionInput().Parse(input)!;
            else if (navigationInput.IsNavigationKey(input))
            {
                if (input == ConsoleKey.Escape)
                {
                    NextPage = Simulator::Page.MenuPage_1;
                    Command = Simulator::Command.Navigate;
                }
                else 
                {
                    Console.Clear();
                    PrintCurrentState(productSelection);
                }
            }
            else if (navigationInput.IsChoiceKey(input))    
                Choose(productSelection);
            else if (navigationInput.IsIterationKey(input))
            {
                productSelection.SelectState(input);
                Console.Clear();
                PrintCurrentState(productSelection);
            }
            else
            {
                Console.Clear();
                PrintCurrentState(productSelection);
            }
        }

        private void Choose(GridMenu productSelection)
        {
            VendingMachine.SetActiveProduct((Products.Product)VendingMachine.AvailableProducts.Where(product => product.GetCategory() == VendingMachine.ActiveCategory).ElementAt(productSelection.Chosen));
            VendingMachine.SetActivePage(Simulator.Page.SubMenuPage_1);
            NextPage = Simulator::Page.ProductInfoPage;
            Command = Simulator::Command.Navigate;

        }
    }
}