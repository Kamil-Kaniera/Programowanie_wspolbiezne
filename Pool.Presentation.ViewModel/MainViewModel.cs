using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Pool.Presentation.Model;

namespace Pool.Presentation.ViewModel;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly IModelApi _modelApi;
    private int _count = 5;

    public bool IsStarted { get; set; }

    public MainViewModel()
    {
        _modelApi = DependencyInjection.Provider.GetRequiredService<IModelApi>();
        StartCommand = new(Start, () => !GetIsStarted());
        StopCommand = new(Stop, GetIsStarted);
    }

    public Commands StartCommand { get; set; }
    public Commands StopCommand { get; set; }

    public int Count
    {
        get => _count;
        set => SetField(ref _count, value);
    }

    public ObservableCollection<BallModel> Balls => _modelApi.ModelBalls;

    public event PropertyChangedEventHandler? PropertyChanged;

    private bool GetIsStarted() => IsStarted;

    private void ToggleIsStarted()
    {
        IsStarted = !IsStarted;
        StartCommand?.OnCanExecuteChanged();
        StopCommand?.OnCanExecuteChanged();
    }

    private async Task Start()
    {
        Balls.Clear();
        _modelApi.Start(Count, Update);
        ToggleIsStarted();

        await Task.CompletedTask;
    }

    private void Update(Guid ballId)
    {
        var updated = _modelApi.GetBall(ballId);
        var existing = Balls.First(b => b.BallId == ballId);

        existing.X = updated.Position.X;
        existing.Y = updated.Position.Y;
    }

    private async Task Stop()
    {
        await _modelApi.ClearBalls();
        ToggleIsStarted();
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}