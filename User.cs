using System;
using Products = Products;
using UI = UI;
using Cash = Cash;

namespace VendingMachineSimulator
{
    public class User
    {
        public UI::IStatusBar StatusBar;
        public Products::Wallet Wallet { get; private set; }
        public Inventory Inventory { get; private set; }
        protected Cash::CashCreator cashCreator;

        public User(Cash::CashCreator cashCreator, VendingMachine vendingMachine)
        {
            this.cashCreator = cashCreator;
            StatusBar = new UI::StatusBar(this);
            Wallet = new Products::Wallet(cashCreator, vendingMachine);
            Inventory = new Inventory(this, Wallet);
        }
        
        public void AddProductToInventory(IProduct product)
        {
            this.Inventory.GetContent().Add(product);
        }
    }
}