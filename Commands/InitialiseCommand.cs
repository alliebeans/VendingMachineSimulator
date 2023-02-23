using System;
using Simulator = VendingMachineSimulator;

namespace Commands
{
    public class InitialiseCommand : Simulator::ICommand
    {
        public void Execute(Simulator::App app) 
        {
            Console.CursorVisible = false;
            Console.Clear();
            app.PageManager.MoveNext(Simulator::Page.IntroPage);
            app.SetInitialised();
        }
    }
}