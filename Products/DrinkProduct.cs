using System;
using UI = UI;
using Simulator = VendingMachineSimulator;

namespace Products
{
    public class DrinkProduct : Product, Simulator::IProduct
    {
        public DrinkProduct(int productId, string name, int weight, int price) : base(productId, name, weight, price) {}
        public override string GetDescription() { return "A refreshing drink to quench your thirst."; }
        public override string GetUserDescription() { return "It's a drink product. Looks wet."; }
        protected Simulator::ProductCategory Category = Simulator::ProductCategory.Drinks;
        public override Simulator::ProductCategory GetCategory() => Category;
        public override string GetCategoryName() => Category.ToString();
        public override void Use(UI::IActionBar actionBar, Simulator::User user) 
        {
            actionBar.Message("You open the can and drink all of it's content.");
            user.Inventory.GetContent().Remove(this);
        }
    }
}