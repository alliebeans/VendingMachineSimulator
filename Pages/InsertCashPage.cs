using System;
using UI;
using UIExtensions;
using Cash = Cash;
using Simulator = VendingMachineSimulator;

namespace Pages
{
    public class InsertCashPage : VendingMachinePage, Simulator::IPage
    {
        public int Amount = 0;
        private string amountString = String.Empty;
        protected CenterLeftRightMenu insertConfirmation;

        public InsertCashPage(Simulator::User user, IActionBar actionBar, Simulator::IInputReader navigationInput, UIDecorator uiDecorator, Simulator::VendingMachine vendingMachine) : base(user, actionBar, navigationInput, uiDecorator, vendingMachine)
        {
            NextPage = VendingMachine.ActivePage;
            insertConfirmation = new CenterLeftRightMenu(UI.Links.LinkClass.CenterLeftRight, "Cancel", "Insert");
            insertConfirmation.Select(1);
        }

        public override void Print()
        {
            UIExtension.HeightPad(initialHeightOffset);
            base.PrintTop();

            UIExtension.HeightPad(heightPad / 2);
            
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            Console.WriteLine("CashInserter™");
            Console.WriteLine();
            
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            Console.Write($"Insert amount: ");

            int _amount;
            Console.CursorVisible = true;
            try 
            {
                _amount = int.Parse(Console.ReadLine()!);
                Amount = _amount;
                amountString = string.Join("", Amount);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }

            Console.CursorVisible = false;

            UIExtension.HeightPad(heightPad / 4);
            uiDecorator.Print(insertConfirmation);
            UIExtension.HeightPad((heightPad) - 3);

            user.StatusBar.Print();
            actionBar.Print();

            Read();
        }

        public void PrintAmount()
        {
            UIExtension.HeightPad(initialHeightOffset);
            base.PrintTop();

            UIExtension.HeightPad(heightPad / 2);
            
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            Console.WriteLine("CashInserter™");
            Console.WriteLine();
            
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            UIExtension.LeftScreenPad();
            Console.WriteLine($"Insert amount: {amountString}");


            UIExtension.HeightPad(heightPad / 4);
            uiDecorator.Print(insertConfirmation);
            UIExtension.HeightPad((heightPad) - 3);

            user.StatusBar.Print();
            actionBar.Print();

            Read();
        }

        private void Read()
        {
            navigationInput.Read();
            var input = navigationInput.GetCurrentKey();

            if (actionBar.GetActionInput().Parse(input) != null)
                Command = (Simulator::Command)actionBar.GetActionInput().Parse(input)!;
            else if (navigationInput.IsNavigationKey(input))
            {
                Command = Simulator.Command.Navigate;
                NextPage = VendingMachine.ActivePage;
            }
            else if (navigationInput.IsChoiceKey(input))
                Choose();
            else if (navigationInput.IsIterationKey(input))
            {
                insertConfirmation.SelectState(input);
                Console.Clear();
                PrintAmount();
            }
            else
            {
                Console.Clear();
                Print();
            }
        }

        private void Choose()
        {
            if (insertConfirmation.Chosen == 1)
            {
                if (user.Wallet.WalletTotal() >= Amount)
                {
                    var addList = user.Wallet.RemoveCashFromWallet(Amount);
                    if (addList.Count() == 0)
                    {
                        actionBar.Message("You search through your wallet...");
                        actionBar.Message("You didn't find any cash with a matching value, try another amount?");
                        Console.Clear();
                        PrintAmount();
                        return;
                    }
                    
                    foreach(Cash::Cash cash in addList) 
                    {
                        VendingMachine.CashInMachine.Add(cash);
                    }
                    
                    Command = Simulator.Command.Navigate;
                    NextPage = VendingMachine.ActivePage;
                }
                else 
                {
                    actionBar.Message("You couldn't find enough cash...");
                    Command = Simulator.Command.Navigate;
                    NextPage = VendingMachine.ActivePage;
                }
            }
            else 
            {
                Command = Simulator.Command.Navigate;
                NextPage = VendingMachine.ActivePage;
            }
        }
    }
}