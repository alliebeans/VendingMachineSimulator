using System;

namespace VendingMachineSimulator
{
    public interface ICommand
    {
        public void Execute(App app);
    }
}