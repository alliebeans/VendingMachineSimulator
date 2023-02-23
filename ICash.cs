using System;

namespace VendingMachineSimulator
{
    public interface ICash
    {
        int GetValue();
        void Pay(VendingMachine vendingMachine);
        void Recieve(User user);
    }
}