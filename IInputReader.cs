using System;

namespace VendingMachineSimulator
{
    public interface IInputReader
    {
        public bool IsIterationKey(ConsoleKey input);
        public bool IsNavigationKey(ConsoleKey input);
        public bool IsChoiceKey(ConsoleKey input);
        ConsoleKey GetCurrentKey();
        void Read();
    }
}