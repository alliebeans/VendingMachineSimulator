using System;
using Simulator = VendingMachineSimulator;

namespace Cash
{
    public class CashCreator
    {
        public static int[] CashValues = {1, 5, 10, 20, 50, 100};
        public Cash Create(Simulator::CashCurrency currency, int value) 
        {
            var _cash = (currency, value) switch 
            {
                (Simulator::CashCurrency.SEK, 1) => new SEK(Simulator::CashForm.Coin, 1),
                (Simulator::CashCurrency.SEK, 5) => new SEK(Simulator::CashForm.Coin, 5),
                (Simulator::CashCurrency.SEK, 10) => new SEK(Simulator::CashForm.Coin, 10),
                (Simulator::CashCurrency.SEK, 20) => new SEK(Simulator::CashForm.Banknote, 20),
                (Simulator::CashCurrency.SEK, 50) => new SEK(Simulator::CashForm.Banknote, 50),
                (Simulator::CashCurrency.SEK, 100) => new SEK(Simulator::CashForm.Banknote, 100),
                _ => throw new IOException()
            };
            return _cash;
        }

        public Cash[] CreateRange(Simulator::CashCurrency currency, int value, int quantity) 
        {
            if (currency == Simulator.CashCurrency.SEK)
            {
                SEK[] range = new SEK[quantity];

                for(int i = 0; i < quantity; i++)
                    range[i] = (SEK)Create(currency, value);
                return range;
            }
            throw new IOException();
        }
    }
}