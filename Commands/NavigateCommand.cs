using System;
using Simulator = VendingMachineSimulator;

namespace Commands
{
    public class NavigateCommand : Simulator::ICommand
    {
        public void Execute(Simulator::App app) {app.PageManager.MoveNext(app.PageManager.NextPage);}
    }
}