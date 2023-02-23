using System;
using UI = UI;
using Simulator = VendingMachineSimulator;

namespace Products
{
    public class HealthyProduct : Product, Simulator::IProduct
    {
        public HealthyProduct(int productId, string name, int weight, int price) : base(productId, name, weight, price) {}
        public override string GetDescription() { return "A healthy alternative for persons with an active lifestyle"; }
        public override string GetUserDescription() { return "It's a decent snack. Snacks doesn't have to be greasy..."; }
        protected Simulator::ProductCategory Category = Simulator::ProductCategory.Healthy;
        public override Simulator::ProductCategory GetCategory() => Category;
        public override string GetCategoryName() => Category.ToString();
        public override void Use(UI::IActionBar actionBar, Simulator::User user) 
        {
            actionBar.Message("You eat the product, still feeling hungry.");
            user.Inventory.GetContent().Remove(this);
        }
    }
}