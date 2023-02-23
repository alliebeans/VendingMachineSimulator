using System;
using InputReaders = InputReaders;
using Simulator = VendingMachineSimulator;

namespace UI
{
    public class ActionBarCreator
    {
        public ActionBar Create(InputReaders::ActionInput[] actions)
        {
            var _actions = new InputReaders::ActionParser(actions);
            return new ActionBar(_actions);
        }
    }
}