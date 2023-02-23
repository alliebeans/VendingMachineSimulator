using System;
using UI = UI;

namespace VendingMachineSimulator
{
    public interface IProduct
    {
        int GetId();
        string GetName();
        ProductCategory GetCategory();
        string GetCategoryName();
        string GetDescription();
        void AddToCart();
        void Buy(User user);
        void Use(UI::IActionBar actionBar, User user);
        string GetUserDescription();
    }
}