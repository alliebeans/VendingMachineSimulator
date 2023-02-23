using System;
using UI;
using UIExtensions;
using VendingMachineSimulator;
using Simulator = VendingMachineSimulator;

namespace Pages
{
    public class ProductInfoPage : VendingMachinePage, Simulator::IPage
    {
        protected CenterLeftRightMenu addToCartConfirmation;
        public ProductInfoPage(User user, IActionBar actionBar, IInputReader navigationInput, UIDecorator uiDecorator, VendingMachine vendingMachine) : base(user, actionBar, navigationInput, uiDecorator, vendingMachine)
        {
            addToCartConfirmation = new CenterLeftRightMenu(UI.Links.LinkClass.CenterLeftRight, "✖", "Add to cart");
            addToCartConfirmation.Select(1);
        }

        public override void Print()
        {
            UIExtension.HeightPad(initialHeightOffset);
            base.PrintTop();
            
            UIExtension.HeightPad(heightPad / 2);

            var category = VendingMachine.ActiveCategory.ToString();
            var itemName = VendingMachine.ActiveProduct!.GetName();
            var weight = VendingMachine.ActiveProduct.Weight;
            var description = VendingMachine.ActiveProduct.GetDescription();
            var price = VendingMachine.ActiveProduct.Price.ToString("C", UIExtension.SE());

            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            Console.WriteLine(category);

            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            Console.WriteLine($"{itemName.ToUpper()}");

            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            Console.WriteLine($"{weight.ToString()}gr");
            

            Console.WriteLine();

            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            Console.WriteLine(description);

            Console.WriteLine();

            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            Console.WriteLine(price);

            UIExtension.HeightPad(3);

            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            uiDecorator.Print(addToCartConfirmation);

            UIExtension.HeightPad(2);
            
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
                Command = (Simulator::Command)actionBar.GetActionInput().Parse(input)!;
            }    
            else if (navigationInput.IsNavigationKey(input))
            {
                Command = Simulator.Command.Navigate;
                NextPage = Simulator.Page.SubMenuPage_1;
            }
            else if (navigationInput.IsChoiceKey(input))
                Choose();
            else if (navigationInput.IsIterationKey(input))
            {
                addToCartConfirmation.SelectState(input);
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
            if (addToCartConfirmation.Chosen == 1)
            {
                VendingMachine.ProductsInCart.Add(VendingMachine.ActiveProduct!);
            
                NextPage = Simulator::Page.MenuPage_1;
                Command = Simulator::Command.Navigate;
            }
            else if (addToCartConfirmation.Chosen == 0)
            {
                NextPage = Simulator::Page.SubMenuPage_1;
                Command = Simulator::Command.Navigate;
            }
        }
    }
}