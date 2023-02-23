using System;

namespace VendingMachineSimulator
{
    public class Displayer
    {
        public void Display(IPage page)
        {
            page.Print();
        }
    }
}