using System;
using Products;
using Cash = Cash;

namespace VendingMachineSimulator
{
    public class VendingMachine
    {
        private User user;
        public Cash::CashCreator CashCreator;
        public List<Cash::Cash> CashInMachine;
        public List<IProduct> AvailableProducts { get; private set; }
        public List<Products.Product> ProductsInCart = new List<Products.Product>();
        public int ProductIdCount { get; private set; }
        public int CashBalance => CashInMachine.Select(cash => cash.LocalValue).Sum();

        public ProductCategory ActiveCategory { get; private set; }
        public Product? ActiveProduct { get; private set; }
        public void SetActiveCategory(ProductCategory category) => ActiveCategory = category;
        public void SetActiveProduct(Product product) => ActiveProduct = product;

        public Page ActivePage { get; private set; }
        public void SetActivePage(Page page) => ActivePage = page;

        public int PurchaseTotalToPay()
        {
            return CashTotal() - CashBalance;
        }

        public int CashTotal()
        {
            var _total = 0;
            foreach(Products.Product p in ProductsInCart)
                _total += p.Price;
            return _total;
        }

        public List<Cash::Cash> GetChangeFromVendingMachine(int amountToGet)
        {
            var _cashRange = Cash::CashCreator.CashValues;
            List<Cash::Cash> cashBack = new List<Cash::Cash>();
            
            for(int i = _cashRange.Length-1; i >= 0; i--)
            {
                if (amountToGet == 0)
                {
                    return cashBack;
                }
                var amountDividedByValue = amountToGet / _cashRange[i];
                if (amountDividedByValue > 0)
                    for(int j = 0; j < amountToGet / _cashRange[i]; j++)
                    {
                        cashBack.Add(CashCreator.Create(CashCurrency.SEK, _cashRange[i]));
                    }
                    amountToGet = amountToGet % _cashRange[i];
            }
            return cashBack;
        }

        public VendingMachine(User user, Cash::CashCreator cashCreator)
        {
            this.user = user;
            this.CashCreator = cashCreator;
            ProductIdCount = 1;

            AvailableProducts = new List<IProduct> 
            {
                new DrinkProduct(ProductIdCount++, "Coca Cola", 33, 10),
                new DrinkProduct(ProductIdCount++, "Sprite", 33, 10),
                new DrinkProduct(ProductIdCount++, "Fanta, apelsin", 33, 10),
                new DrinkProduct(ProductIdCount++, "Fanta, exotic", 33, 10),
                new DrinkProduct(ProductIdCount++, "Julmust", 33, 10),
                new DrinkProduct(ProductIdCount++, "Pepsi", 33, 10),
                new DrinkProduct(ProductIdCount++, "Ramlösa, original", 33, 10),
                new DrinkProduct(ProductIdCount++, "Ramlösa, citrus", 33, 10),
                new DrinkProduct(ProductIdCount++, "Dr Pepper", 33, 10),

                new CandyProduct(ProductIdCount++, "Ahlgrens bilar", 160, 25),
                new CandyProduct(ProductIdCount++, "Kexchoklad", 55, 10),
                new CandyProduct(ProductIdCount++, "Snickers", 55, 10),
                new CandyProduct(ProductIdCount++, "Lion", 55, 10),
                new CandyProduct(ProductIdCount++, "Nappar, fruit", 80, 10),

                new SnackProduct(ProductIdCount++, "Cheez Doodles", 35, 8),
                new SnackProduct(ProductIdCount++, "Chips, grill", 35, 8),
                new SnackProduct(ProductIdCount++, "Chips, dill", 35, 8),
                new SnackProduct(ProductIdCount++, "Pringles, original", 55, 15),
                new SnackProduct(ProductIdCount++, "Pringles, sourcream & onion", 55, 15),

                new PastryProduct(ProductIdCount++, "Delicatoboll", 8, 10),
                new PastryProduct(ProductIdCount++, "Punschrulle", 8, 10),
                new PastryProduct(ProductIdCount++, "Pepparkaksrulle", 8, 10),

                new HealthyProduct(ProductIdCount++, "Nutrilett naturdiet, choklad", 20, 15),
                new HealthyProduct(ProductIdCount++, "Knäckebröd, paprika", 22, 10),
                new HealthyProduct(ProductIdCount++, "Knäckebröd, french herbs", 22, 10),
                new HealthyProduct(ProductIdCount++, "Riskakor mini, ost", 5, 10),
                new HealthyProduct(ProductIdCount++, "Nötter, cashew", 15, 15),
            };

            CashInMachine = new List<Cash::Cash>();
        }
    }
}