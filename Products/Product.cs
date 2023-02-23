using System;
using UI = UI;
using Simulator = VendingMachineSimulator;

namespace Products
{
    public abstract class Product : Simulator::IProduct
    {
        public virtual int ProductId { get; protected set; }
        public virtual string Name { get; protected set; }
        public virtual int Weight { get; protected set; }
        public virtual int Price { get; protected set; }

        public Product(int productId, string name, int weight, int price) 
        {
            Name = name;
            Weight = weight;
            Price = price;
            ProductId = productId;
        }

        public int GetId() => ProductId;
        public string GetName() => this.Name;
        public abstract string GetCategoryName();
        public abstract Simulator::ProductCategory GetCategory();
        public void AddToCart() {throw new NotImplementedException();}
        public void Buy(Simulator::User user) { user.Inventory.GetContent().Add(this); }
        public virtual string GetDescription() { return "An amazing product."; }
        public virtual string GetUserDescription() { return "It's a random product."; }
        public abstract void Use(UI::IActionBar actionBar, Simulator::User user);
    }
}