using System;
using Simulator = VendingMachineSimulator;

namespace Commands
{
    public class ShowInsertCashCommand : Simulator::ICommand
    {
        public void Execute(Simulator::App app) => app.PageManager.MoveNext(Simulator::Page.InsertCashPage);
    }
}