using System;
using VendingMachineSimulator;
using Simulator = VendingMachineSimulator;

namespace Cash
{
    public class SEK : Cash, Simulator::ICash
    {
        public SEK(CashForm cashForm, int localValue) : base(cashForm, localValue) {}
    }
}