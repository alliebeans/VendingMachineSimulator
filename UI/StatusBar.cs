using System;
using System.Threading;
using UIExtensions;
using Simulator = VendingMachineSimulator;

namespace UI
{
    public class StatusBar : IStatusBar
    {
        private Simulator::User User;
        private bool IsInitialised = false;
        public StatusBar(Simulator::User user)
        {
            User = user;
        }
        public void Print() => DisplayStatusBar();

        private void DisplayStatusBar()
        {
            string _total = $"Wallet Total: {User.Wallet.WalletTotal().ToString("C", UIExtensions.UIExtension.SE())}";
            string _time = $"{DateTime.Now.ToShortTimeString()}";
            Console.Write($"{_total}{_time.PadLeft(UIExtension.Width - _total.Length)}");
            Console.WriteLine();
        }
    }
}