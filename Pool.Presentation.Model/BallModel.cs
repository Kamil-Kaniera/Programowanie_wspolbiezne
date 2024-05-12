using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Pool.Presentation.Model;

public class BallModel(Guid ballId, int x, int y, int diameter) : INotifyPropertyChanged
{
    public Guid BallId => ballId;

    public int X
    {
        get => x;
        set => SetField(ref x, value);
    }

    public int Y
    {
        get => y;
        set => SetField(ref y, value);
    }

    public int Diameter => diameter;

    public event PropertyChangedEventHandler? PropertyChanged;

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