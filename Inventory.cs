using System;
using Products;


namespace VendingMachineSimulator
{
    public class Inventory
    {
        private User User;
        private List<IProduct> ProductsInInventory = new List<IProduct>();
        public List<IProduct> GetContent() => ProductsInInventory;
        public IProduct GetProduct(int product) => ProductsInInventory[product];
        public Inventory(User user, Wallet wallet)
        {
            User = user;
            ProductsInInventory.Add(wallet);
        }
    }
}