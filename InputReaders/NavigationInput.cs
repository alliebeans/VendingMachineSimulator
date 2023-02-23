using System;
using Simulator = VendingMachineSimulator;

namespace InputReaders
{
    public class NavigationInput : Simulator::IInputReader
    {
        public ConsoleKey CurrentKey;
        public ConsoleKey GetCurrentKey() => CurrentKey;
        private static ConsoleKey[] _iterationKeys = {ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.UpArrow, ConsoleKey.DownArrow};
        private static ConsoleKey[] _navigationKeys = {ConsoleKey.Escape, ConsoleKey.Tab};
        private static ConsoleKey[] _choiceKeys = {ConsoleKey.Enter, ConsoleKey.Spacebar};
        public bool IsIterationKey(ConsoleKey input) => _iterationKeys.Contains(input);
        public bool IsNavigationKey(ConsoleKey input) => _navigationKeys.Contains(input);
        public bool IsChoiceKey(ConsoleKey input) => _choiceKeys.Contains(input);
        
        
        public void Read()
        {
            var input = Console.ReadKey(true);
            CurrentKey = input.Key;
            return;
        }
    }
}