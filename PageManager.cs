using System;
using UI = UI;

namespace VendingMachineSimulator
{
    public class PageManager
    {
        public IPage? CurrentPage { get; private set; }
        public Page NextPage;
        Dictionary<Page, IPage> AppPages;

        private User user;
        private UI::ActionBarCreator actionBarCreator;
        private UI::UIDecorator uiDecorator;
        private InputReaders.NavigationInput navigationInput;
        private VendingMachine vendingMachine;
        public PageManager(User user, UI::ActionBarCreator actionBarCreator, InputReaders.NavigationInput navigationInput, UI::UIDecorator uiDecorator, VendingMachine vendingMachine)
        {
            this.user = user;
            this.actionBarCreator = actionBarCreator;
            this.uiDecorator = uiDecorator;
            this.navigationInput = navigationInput;
            this.vendingMachine = vendingMachine;

            AppPages = new Dictionary<Page, IPage> 
            {
                {Page.IntroPage, new Pages.IntroPage(user, actionBarCreator.Create(new[] {InputReaders.ActionInput.Inventory}), navigationInput, uiDecorator)},
                {Page.InFrontOfVendingMachinePage, new Pages.InFrontOfVendingMachinePage(user, actionBarCreator.Create(new[] {InputReaders.ActionInput.Quit_Program, InputReaders.ActionInput.Inventory, InputReaders.ActionInput.Use}), navigationInput, uiDecorator)},
                {Page.InventoryPage, new Pages.InventoryPage(user, actionBarCreator.Create(new[] {InputReaders.ActionInput.Leave, InputReaders.ActionInput.Description, InputReaders.ActionInput.Use}), navigationInput, uiDecorator)},
                {Page.MenuPage_1, new Pages.MenuPage_1(user, actionBarCreator.Create(new[] {InputReaders.ActionInput.Leave, InputReaders.ActionInput.Insert_Cash, InputReaders.ActionInput.Checkout, InputReaders.ActionInput.Select}), navigationInput, uiDecorator, vendingMachine)},
                {Page.MenuPage_2, new Pages.MenuPage_2(user, actionBarCreator.Create(new[] {InputReaders.ActionInput.Leave, InputReaders.ActionInput.Insert_Cash, InputReaders.ActionInput.Checkout, InputReaders.ActionInput.Select}), navigationInput, uiDecorator, vendingMachine)},
                {Page.SubMenuPage_1, new Pages.SubMenuPage_1(user, actionBarCreator.Create(new[] {InputReaders.ActionInput.Back, InputReaders.ActionInput.Insert_Cash, InputReaders.ActionInput.Select}), navigationInput, uiDecorator, vendingMachine)},
                {Page.ProductInfoPage, new Pages.ProductInfoPage(user, actionBarCreator.Create(new[] {InputReaders.ActionInput.Close, InputReaders.ActionInput.Select}), navigationInput, uiDecorator, vendingMachine)},
                {Page.InsertCashPage, new Pages.InsertCashPage(user, actionBarCreator.Create(new[] {InputReaders.ActionInput.Back, InputReaders.ActionInput.Inventory}), navigationInput, uiDecorator, vendingMachine)},
                {Page.CheckoutPage_1, new Pages.CheckoutPage_1(user, actionBarCreator.Create(new[] {InputReaders.ActionInput.Back, InputReaders.ActionInput.Insert_Cash, InputReaders.ActionInput.Select}), navigationInput, uiDecorator, vendingMachine)},
            };
        }

        public IPage GetNext(Page page)
        {
            IPage _next;
            if (!AppPages.TryGetValue(page, out _next!))
                throw new IOException();
            return _next;
        }

        public IPage MoveNext(Page page)
        {
            CurrentPage = GetNext(page);
            Console.Clear();
            return CurrentPage;
        }
    }
}