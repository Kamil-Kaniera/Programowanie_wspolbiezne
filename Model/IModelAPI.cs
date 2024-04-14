using Logic;
using System.Collections.ObjectModel;

namespace Model;

public interface IModelAPI
{
     static IModelAPI CreateAPI() => new ModelAPI(ILogicAPI.createAPI());

     abstract void Start(int ballsAmount);

     abstract void ClearBalls();

     abstract void GetCircles();

}