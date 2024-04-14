using System.Windows.Input;

namespace ViewModel;

public class Commands : ICommand
{
    private readonly Action _action;

    public Commands(Action action)
    {
        _action = action;
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        _action.Invoke();
    }

    public event EventHandler? CanExecuteChanged;
}
