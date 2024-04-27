using System.Collections.ObjectModel;
using Pool.Common.Model;

namespace Pool.Presentation.Model;

public interface IModelApi
{
    ObservableCollection<BallModel> ModelBalls { get; }
    void Start(int ballsAmount, Action<Guid> callback);

    Task ClearBalls();

    void BuildBallModels();
    Ball GetBall(Guid ballId);
}