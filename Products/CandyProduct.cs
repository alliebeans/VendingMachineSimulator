using System;
using UI = UI;
using Simulator = VendingMachineSimulator;

namespace Products
{
    public class CandyProduct : Product, Simulator::IProduct
    {
        public CandyProduct(int productId, string name, int weight, int price) : base(productId, name, weight, price) {}
        public override string GetDescription() { return "This candy will feed the need for sugar in your life."; }
        public override string GetUserDescription() { return "It's candy. Looks tasty!"; }
        protected Simulator::ProductCategory Category = Simulator::ProductCategory.Candy;
        public override Simulator::ProductCategory GetCategory() => Category;
        public override string GetCategoryName() => Category.ToString();
        public override void Use(UI::IActionBar actionBar, Simulator::User user) 
        {
            actionBar.Message("You eat the candy. Sugar rush!");
            user.Inventory.GetContent().Remove(this);
        }
    }
}