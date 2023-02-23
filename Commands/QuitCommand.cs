using System;
using Simulator = VendingMachineSimulator;

namespace Commands
{
    public class QuitCommand : Simulator::ICommand
    {
        public void Execute(Simulator::App app) 
        {
            Console.Clear();
            Console.CursorVisible = true;
            app.RequestQuit();
        }
    }
}