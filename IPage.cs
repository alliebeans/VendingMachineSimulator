using System;

namespace VendingMachineSimulator
{
    public interface IPage
    {
        public Page GetNextPage();
        public Command GetCommand();
        public void Print();
    }
}