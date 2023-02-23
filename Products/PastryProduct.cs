using System;
using UI = UI;
using Simulator = VendingMachineSimulator;
using VendingMachineSimulator;

namespace Products
{
    public class PastryProduct : Product, Simulator::IProduct
    {
        public PastryProduct(int productId, string name, int weight, int price) : base(productId, name, weight, price) {}
        public override string GetDescription() { return "A wonderful pastry for a relaxing and tasty moment."; }
        public override string GetUserDescription() { return "It's a pastry. Is it fika-time yet?"; }
        protected Simulator::ProductCategory Category = Simulator::ProductCategory.Pastries;
        public override Simulator::ProductCategory GetCategory() => Category;
        public override string GetCategoryName() => Category.ToString();
        public override void Use(UI::IActionBar actionBar, Simulator::User user) 
        {
            actionBar.Message("You eat the pastry. Delicious!");
            user.Inventory.GetContent().Remove(this);
        }
    }
}