using Logic;
using System.Collections.ObjectModel;

namespace Model;

public interface IModelAPI
{
     static IModelAPI CreateAPI() => new ModelAPI(ILogicAPI.createAPI());

     abstract void Start(int BallsAmount, int Radius);

     abstract void ClearBalls();

     abstract ObservableCollection<BallModel> GetCircles();

}