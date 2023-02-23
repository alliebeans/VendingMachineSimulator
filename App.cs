using System;

namespace VendingMachineSimulator
{
    public class App
    {
        #region Commands
        private ICommand InitialiseCommand = new Commands.InitialiseCommand();
        private ICommand ShowInventoryCommand = new Commands.ShowInventoryCommand();
        private ICommand ShowInFrontOfVendingMachineCommand = new Commands.ShowInFrontOfVendingMachineCommand();
        private ICommand ShowInsertCashCommand = new Commands.ShowInsertCashCommand();
        private ICommand NavigateCommand = new Commands.NavigateCommand();
        private ICommand QuitCommand = new Commands.QuitCommand();
        #endregion
        
        #region Local methods
        public bool IsInitialised = false;
        public bool QuitRequested = false;
        public void RequestQuit() => QuitRequested = true;
        public void SetInitialised() => IsInitialised = true;
        #endregion
        
        #region Components
        public User User { get; private set; }
        public Displayer Displayer { get; private set; }
        public PageManager PageManager { get; private set; }
        public UI.ActionBarCreator ActionBarCreator { get; private set; }
        public UI.UIDecorator UIDecorator { get; private set; }
        public InputReaders.NavigationInput NavigationInput { get; private set; }
        public Cash.CashCreator CashCreator { get; private set; }
        public VendingMachine VendingMachine { get; private set; }
        #endregion

        public App()
        {
            Displayer = new Displayer();
            CashCreator = new Cash.CashCreator();
            User = new User(CashCreator, VendingMachine = new VendingMachine(User!, CashCreator));
            ActionBarCreator = new UI.ActionBarCreator();
            UIDecorator = new UI.UIDecorator();
            NavigationInput = new InputReaders.NavigationInput();

            PageManager = new PageManager(User, ActionBarCreator, NavigationInput, UIDecorator, VendingMachine);
        }

        public void Init()
        {
            InitialiseCommand.Execute(this);

            if (IsInitialised)
                Run();
        }
        
        public void Run()
        {
            while (!QuitRequested)
            {
                var currentPage = PageManager.CurrentPage;
                Displayer.Display(currentPage!);

                var nextPage = currentPage!.GetNextPage();
                PageManager.NextPage = nextPage;

                var command = currentPage!.GetCommand() switch
                {
                    Command.Initialise => InitialiseCommand,
                    Command.ShowInFrontOfVendingMachineCommand => ShowInFrontOfVendingMachineCommand,
                    Command.ShowInventory => ShowInventoryCommand,
                    Command.ShowInsertCash => ShowInsertCashCommand,
                    Command.Navigate => NavigateCommand,
                    Command.Quit => QuitCommand,
                    _ => throw new Exception(),
                };

                command.Execute(this);
            }
            return;
        }
    }
}