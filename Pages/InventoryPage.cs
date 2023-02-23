using System;
using UI;
using UIExtensions;
using Simulator = VendingMachineSimulator;

namespace Pages
{
    public class InventoryPage : Page, Simulator::IPage
    {
        private static int heightPad = UIExtension.Height - 4;
        private int padOffset = 1;
        private static int initialHeightOffset = 5;
        private int LastCount = 1;
        private int ItemsCount() => this.user.Inventory.GetContent().Count();
        private bool WasUpdated => LastCount != ItemsCount() ? true : false;
        public InventoryPage(Simulator.User user, IActionBar actionBar, Simulator.IInputReader navigationInput, UIDecorator uiDecorator) : base(user, actionBar, navigationInput, uiDecorator)
        {
            Command = Simulator::Command.Navigate;
            NextPage = Simulator::Page.InventoryPage;
            heightPad -= initialHeightOffset + user.Inventory.GetContent().Count();
        }

        public override void Print()
        {
            var inventoryItems = GetInventoryItems();

            if (WasUpdated)
            {
                padOffset = 0;
                padOffset = user.Inventory.GetContent().Count() / 2;
                LastCount = ItemsCount();
            }
            PrintCurrentState(inventoryItems);
            
        }

        private VerticalCenterMenu GetInventoryItems()
        {
            string[] productNames = user.Inventory.GetContent().Select(product => product.GetName()).ToArray();
            VerticalCenterMenu inventoryItems = new VerticalCenterMenu(UI.Links.LinkClass.VerticalCenter, productNames);
            inventoryItems.Select(0);
            return inventoryItems;
        }

        private void PrintCurrentState(VerticalCenterMenu inventoryItems)
        {
            UIExtension.HeightPad(initialHeightOffset);
            UIExtension.LeftScreenPad();
            Console.WriteLine("INVENTORY");
            Console.WriteLine();
            UIExtension.HeightPad(heightPad / 2 - padOffset - 2);
            uiDecorator.Print(inventoryItems);
            UIExtension.HeightPad(heightPad / 2 - padOffset);
            if (user.Inventory.GetContent().Count() % 2 == 0)
                Console.WriteLine();

            user.StatusBar.Print();
            actionBar.Print();

            Read(inventoryItems);
        }

        private void Read(VerticalCenterMenu inventoryItems)
        {
            navigationInput.Read();
            var input = navigationInput.GetCurrentKey();
            
            if (navigationInput.IsNavigationKey(input))
                NextPage = Simulator.Page.InFrontOfVendingMachinePage;
            else if (navigationInput.IsIterationKey(input))
            {
                inventoryItems.SelectState(input);
                Console.Clear();
                PrintCurrentState(inventoryItems);
            }
            else if (input == ConsoleKey.D)
            {
                actionBar.Message(user.Inventory.GetProduct(inventoryItems.Chosen).GetUserDescription());
                Console.Clear();
                PrintCurrentState(inventoryItems);
            }
            else if (navigationInput.IsChoiceKey(input))
            {
                user.Inventory.GetProduct(inventoryItems.Chosen).Use(actionBar, user);
                Console.Clear();
                Print();
            }
            else
            {
                Console.Clear();
                PrintCurrentState(inventoryItems);
            }
        }
    }
}