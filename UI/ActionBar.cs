using System;
using UIExtensions;
using InputReaders = InputReaders;
using Simulator = VendingMachineSimulator;

namespace UI
{
    public class ActionBar : IActionBar, IUIComponent
    {
        private string CurrentMessage = String.Empty;
        public string NextMessage(string msg) => CurrentMessage = msg;
        private InputReaders::ActionParser ActionInput;
        public InputReaders::ActionParser GetActionInput() => ActionInput;
        public ActionBar(InputReaders::ActionParser actionInput)
        {
            ActionInput = actionInput;
        }
        public void Print() 
        {
            if (String.IsNullOrEmpty(CurrentMessage))
            {
                for(int i = 0; i < ActionInput.Available.Length - 1; i++)
                    Console.Write($"[{ActionInput.Available[i].Item1.ToString()}] {ActionInput.Available[i].Item2}   ");

                Console.Write($"[{ActionInput.Available[ActionInput.Available.Length-1].Item1.ToString()}] {ActionInput.Available[ActionInput.Available.Length-1].Item2}");
            }
            else
                Message(CurrentMessage);
        }

        public void Message(string msg)
        {
            UIExtension.ClearLine();
            Thread.Sleep(150);
            Console.Write($"{msg}");
            Thread.Sleep(2850);
            UIExtension.ClearLine();
            CurrentMessage = String.Empty;
            Print();
            return;
        }
    }
}