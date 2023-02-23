using System;
using UI = UI;
using Simulator = VendingMachineSimulator;
using VendingMachineSimulator;

namespace Products
{
    public class SnackProduct : Product, Simulator::IProduct
    {
        public SnackProduct(int productId, string name, int weight, int price) : base(productId, name, weight, price) {}
        public override string GetDescription() { return "A quick bite that will entice your taste buds."; }
        public override string GetUserDescription() { return "It's a snack. Looks greasy."; }
        protected Simulator::ProductCategory Category = Simulator::ProductCategory.Snacks;
        public override Simulator::ProductCategory GetCategory() => Category;
        public override string GetCategoryName() => Category.ToString();
        public override void Use(UI::IActionBar actionBar, Simulator::User user) 
        {
            actionBar.Message("Mmm... Greasy!");
            user.Inventory.GetContent().Remove(this);
        }
    }
}