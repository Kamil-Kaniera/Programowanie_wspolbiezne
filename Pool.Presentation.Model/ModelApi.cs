using System.Collections.ObjectModel;
using Pool.Common.Model;
using Pool.Logic.Abstract;

namespace Pool.Presentation.Model;

public class ModelApi(ILogicApi logicApi) : IModelApi
{
    public ObservableCollection<BallModel> ModelBalls { get; } = new();

    public async Task ClearBalls()
    {
        await logicApi.StopMovement();
        ModelBalls.Clear();
    }

    public void BuildBallModels()
    {
        foreach (var s in logicApi.Balls)
            ModelBalls.Add(new(s.BallId, s.Position.X, s.Position.Y, s.Radius));
    }

    public Ball GetBall(Guid ballId)
    {
        return logicApi.GetBall(ballId);
    }


    public void Start(int ballsAmount, Action<Guid> callback)
    {
        ModelBalls.Clear();
        logicApi.CreateBalls(ballsAmount);
        BuildBallModels();
        logicApi.StartMovement(callback);
    }
}