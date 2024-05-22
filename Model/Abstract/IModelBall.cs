using Logic.Abstract;
using System.ComponentModel;

namespace Model.Abstract
{
    public interface IModelBall : INotifyPropertyChanged, IObserver<ILogicBall>
    {
        int X { get; }
        int Y { get; }
        int Diameter { get; } 
    }
}
