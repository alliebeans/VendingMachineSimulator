using System;
using Simulator = VendingMachineSimulator;

namespace InputReaders
{
    public class ActionParser
    {
        public Dictionary<ActionInput, (ConsoleKey, string)> Actions { get; private set; }
        public (ConsoleKey, string)[] Available;
        private ConsoleKey[] AvailableKeys;

        public ActionParser(params ActionInput[] available)
        {
            Actions = new Dictionary<ActionInput, (ConsoleKey, string)>
            {
                {ActionInput.Quit_Program, (ConsoleKey.Q, "Quit Program")},
                {ActionInput.Inventory, (ConsoleKey.I, "Inventory")},
                {ActionInput.Description, (ConsoleKey.D, "Show description")},
                {ActionInput.Use, (ConsoleKey.Enter, "Use")},
                {ActionInput.Leave, (ConsoleKey.Escape, "Leave")},
                {ActionInput.Checkout, (ConsoleKey.Tab, "Checkout")},
                {ActionInput.Insert_Cash, (ConsoleKey.C, "Insert Cash")},
                {ActionInput.Select, (ConsoleKey.Enter, "Select")},
                {ActionInput.Back, (ConsoleKey.Escape, "Back")},
                {ActionInput.Close, (ConsoleKey.Escape, "Close")},
            };

            var _available = new (ConsoleKey, string)[available.Length];
            
            for(int i = 0;i < available.Length; i++)
                _available[i] = TryGetAction(available[i]);

            Available = _available;

            var _availableKeys = new ConsoleKey[available.Length];
            
            for(int i = 0;i < available.Length; i++)
                _availableKeys[i] = TryGetKey(available[i]);

            AvailableKeys = _availableKeys;
        }

        public (ConsoleKey, string) TryGetAction(ActionInput input) 
        {
            (ConsoleKey, string) action;
            if (!Actions.TryGetValue(input, out action!))
                throw new IOException();
            return action;
        }

        public ConsoleKey TryGetKey(ActionInput input) 
        {
            (ConsoleKey, string) action;
            if (!Actions.TryGetValue(input, out action!))
                throw new IOException();
            return action.Item1;
        }

        public Simulator::Command? Parse(ConsoleKey input) 
        {
            var ActionCommands = new Dictionary<ConsoleKey, Simulator::Command>
            {
                //ActionInputs.Quit_Game
                {ConsoleKey.Q, Simulator.Command.Quit},
                //ActionInputs.Inventory
                {ConsoleKey.I, Simulator.Command.ShowInventory},
                //ActionInputs.Insert_Cash
                {ConsoleKey.C, Simulator.Command.ShowInsertCash},
            };
            
            if (AvailableKeys.Contains(input)) 
            {
                Simulator.Command command;
                if (!ActionCommands.TryGetValue(input, out command!))
                    return null;
                return command;
            }
            return null;
        }
    }
}