using System.Collections.ObjectModel;

namespace Model.Abstract
{
    public interface IModelApi
    {
        public void Start(int numberOfBalls);
        public void Stop();
        ObservableCollection<IModelBall> ModelBalls { get; }
    }
}
