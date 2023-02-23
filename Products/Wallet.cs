using System;
using System.Linq;
using Simulator = VendingMachineSimulator;
using Cash = Cash;
using UI = UI;
using VendingMachineSimulator;
using System.Text;

namespace Products
{
    public class Wallet : Simulator::IProduct
    {
        public int GetId() => 0;
        public List<Cash::Cash> CashInWallet;
        public string Name;
        public string GetName() => Name;
        public int Weight => CashInWallet.Where(cash => cash.CashForm == Simulator.CashForm.Coin).Count();
        public int Price;
        private Cash::CashCreator cashCreator;
        private VendingMachine vendingMachine;
        public Wallet(Cash::CashCreator cashCreator, VendingMachine vendingMachine) 
        {
            this.vendingMachine = vendingMachine;
            this.cashCreator = cashCreator;
            Name = "Wallet";
            Price = 0;
            CashInWallet = new List<Cash::Cash>();
            InitStartingCash();
        }

        private void InitStartingCash()
        {
            CashInWallet.AddRange(cashCreator.CreateRange(Simulator.CashCurrency.SEK, 1, 10));
            CashInWallet.AddRange(cashCreator.CreateRange(Simulator.CashCurrency.SEK, 5, 10));
            CashInWallet.AddRange(cashCreator.CreateRange(Simulator.CashCurrency.SEK, 10, 10));
        }

        public List<Cash::Cash> RemoveCashFromWallet(int amountToRemove)
        {
            var _cashValues = Cash::CashCreator.CashValues;
            var _cashToMachine = new List<Cash::Cash>();
            var originalAmount = amountToRemove;

            for (int i = _cashValues.Length-1; i >= 0; i--)
            {
                var amountDividedByValue = amountToRemove / _cashValues[i];
                var valueCountInWallet = CashInWallet.Where(cash => cash.LocalValue == _cashValues[i]).Count();
                
                if (amountToRemove == 0)
                    return _cashToMachine;

                if (amountDividedByValue > 0 && valueCountInWallet > 0)
                {
                    if (valueCountInWallet >= amountDividedByValue)
                    {
                        for(int j = 0; j < amountDividedByValue; j++)
                        {
                            CashInWallet.Remove(CashInWallet.Find(cash => cash.LocalValue == _cashValues[i])!);
                            _cashToMachine.Add(cashCreator.Create(Simulator::CashCurrency.SEK, _cashValues[i]));
                        }
                        amountToRemove = amountToRemove % _cashValues[i];
                    }
                    else if (amountDividedByValue > valueCountInWallet)
                    {
                        for(int j = 0; j < valueCountInWallet; j++)
                        {
                            CashInWallet.Remove(CashInWallet.Find(cash => cash.LocalValue == _cashValues[i])!);
                            _cashToMachine.Add(cashCreator.Create(Simulator::CashCurrency.SEK, _cashValues[i]));
                        }
                        amountToRemove -= _cashValues[i] * valueCountInWallet;
                    }
                }
                
            }
            if (amountToRemove != 0)
            {   
                return vendingMachine.GetChangeFromVendingMachine(0);
            }
            return _cashToMachine;
        }
        
        public void AddToCart() {throw new InvalidOperationException();}
        public void Buy(Simulator::User user) {throw new InvalidOperationException();}
        public string GetDescription() 
        {
            return $"It's a faux-leather wallet that weighs {Weight} grams.";
        }
        public string GetUserDescription() 
        {
            return $"It's a faux-leather wallet that weighs {Weight} grams.";
        }
        public string GetCategoryName() => "Accessories";
        public void Use(UI::IActionBar actionBar, Simulator::User user)
        {
            actionBar.Message("You search through your wallet...");

            var count1 = CashInWallet.Where(cash => cash.LocalValue == 1).Count();
            var count5 = CashInWallet.Where(cash => cash.LocalValue == 5).Count();
            var count10 = CashInWallet.Where(cash => cash.LocalValue == 10).Count();
            var count20 = CashInWallet.Where(cash => cash.LocalValue == 20).Count();
            var count50 = CashInWallet.Where(cash => cash.LocalValue == 50).Count();
            var count100 = CashInWallet.Where(cash => cash.LocalValue == 100).Count();

            var msg = new StringBuilder();

            msg.Append($"You find: ");
            if (count100 > 0)
                msg.Append($"({count100}) 100-notes, ");
            if (count50 > 0)
                msg.Append($"({count50}) 50-notes, ");
            if (count20 > 0)
                msg.Append($"({count20}) 20-notes, ");
            if (count10 > 0)
                msg.Append($"({count10}) 10-coins, ");
            if (count5 > 0)
                msg.Append($"({count5}) 5-coins, ");
            if (count1 > 0)
                msg.Append($"({count1}) 1-coins. ");

             actionBar.Message(msg.ToString());
        }
        public int WalletTotal() 
        {
            var total = 0;
            foreach(Cash::Cash cash in CashInWallet)
                total += cash.GetValue();
            return total;
        }

        public ProductCategory GetCategory()
        {
            throw new NotImplementedException();
        }
    }
}