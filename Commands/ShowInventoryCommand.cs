using System;
using Simulator = VendingMachineSimulator;

namespace Commands
{
    public class ShowInventoryCommand : Simulator::ICommand
    {
        public void Execute(Simulator::App app) => app.PageManager.MoveNext(Simulator::Page.InventoryPage);
    }
}