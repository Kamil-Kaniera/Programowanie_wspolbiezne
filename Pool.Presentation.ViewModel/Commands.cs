using System.Windows.Input;

namespace Pool.Presentation.ViewModel;

public class Commands : ICommand
{
    private readonly Func<Task> _action;
    private readonly Func<bool> _getCanExecute;

    public Commands(Func<Task> action, Func<bool> getCanExecute)
    {
        _action = action;
        _getCanExecute = getCanExecute;
    }

    public bool CanExecute(object? parameter)
        => _getCanExecute();

    public async void Execute(object? parameter)
    {
        await _action.Invoke();
    }

    public void OnCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(null, EventArgs.Empty);
    }

    public event EventHandler? CanExecuteChanged;
}