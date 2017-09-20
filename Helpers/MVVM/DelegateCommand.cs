using System;
using System.Windows.Input;

namespace WpfApplicationLiga.Helpers.MVVM
{
    public class DelegateCommand : ICommand
    {
        private readonly Action _action;
        private bool canExecute = true;
        public DelegateCommand(Action action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action();
        }

        public void SetCanExecute(bool value)
        {
            if (this.canExecute != value)
            {
                this.canExecute = value;

                if (this.CanExecuteChanged != null)
                {
                    this.CanExecuteChanged(this, new EventArgs());
                }
            }
        }
    }
}
