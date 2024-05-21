using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public class Commands : ICommand
    {
        private readonly Action _action;
        private readonly Func<bool> _getCanExecute;

        public Commands(Action action, Func<bool> getCanExecute)
        {
            _action = action;
            _getCanExecute = getCanExecute;
        }

        public bool CanExecute(object? parameter)
            => _getCanExecute();

        public void Execute(object? parameter)
        {
            _action.Invoke();
        }

        public void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(null, EventArgs.Empty);
        }

        public event EventHandler? CanExecuteChanged;
    }
}
