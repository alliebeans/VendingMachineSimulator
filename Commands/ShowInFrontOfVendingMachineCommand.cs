using System;
using Simulator = VendingMachineSimulator;

namespace Commands
{
    public class ShowInFrontOfVendingMachineCommand : Simulator::ICommand
    {
        public void Execute(Simulator::App app) => app.PageManager.MoveNext(Simulator::Page.InFrontOfVendingMachinePage);
    }
}