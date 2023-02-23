using System;
using InputReaders = InputReaders;

namespace UI
{
    public interface IActionBar
    {
        void Print();
        InputReaders::ActionParser GetActionInput();
        void Message(string msg);
    }
}