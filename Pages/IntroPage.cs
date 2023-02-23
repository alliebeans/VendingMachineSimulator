using System;
using UI;
using UIExtensions;
using Simulator = VendingMachineSimulator;

namespace Pages
{
    public class IntroPage : Page, Simulator::IPage
    {
        public IntroPage(Simulator.User user, IActionBar actionBar, Simulator.IInputReader navigationInput, UIDecorator uiDecorator) : base(user, actionBar, navigationInput, uiDecorator)
        {
        }

        public override void Print()
        {
            UIExtension.HeightPad(12);
            var msg = "You find yourself in front of a vending machine.";
            var rnd = new Random();
            var number = rnd.Next(0, 4);

            var hungry1 = "You're a bit peckish, you could do with a bite of food...";
            var hungry2 = "You're kind of hungry...";
            var hungry3 = "You're starving, you need some food...";

            var hungry = new[] 
            { 
                hungry1,
                hungry1,
                hungry2,
                hungry3
            };
            Console.WriteLine();

            Console.Write(msg.PadLeft(UIExtension.Width / 2 + (msg.Length / 2)));
            Thread.Sleep(3000);
            UIExtension.ClearLine();
            Console.Write(hungry[number].PadLeft(UIExtension.Width / 2 + (hungry[number].Length / 2)));
            Thread.Sleep(2000);


            NextPage = Simulator.Page.InFrontOfVendingMachinePage;
            Command = Simulator.Command.Navigate;
        }
    }
}