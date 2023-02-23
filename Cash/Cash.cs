using System;
using Simulator = VendingMachineSimulator;

namespace Cash
{
    public abstract class Cash : Simulator::ICash
    {
        public Simulator::CashForm CashForm { get; protected set; }
        public int LocalValue { get; protected set; }
        public int GetValue() => LocalValue;
        public Cash(Simulator::CashForm cashForm, int localValue) { CashForm = cashForm; LocalValue = localValue; }
        public void Recieve(Simulator::User user) 
        {
             user.Wallet.CashInWallet.Add(this);
        }
        public void Pay(Simulator::VendingMachine vendingMachine) 
        {
            throw new NotImplementedException(); 
        }
    }
}