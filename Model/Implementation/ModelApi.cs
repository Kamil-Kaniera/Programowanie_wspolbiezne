using System.Collections.ObjectModel;
using Logic.Abstract;
using Model.Abstract;

namespace Model.Implementation
{
    public class ModelApi(ILogicApi logic) : IModelApi
    {
        private readonly ILogicApi _logicApi = logic;
        public ObservableCollection<IModelBall> ModelBalls { get; } = [];

        public void Start(int numberOfBalls)
        {
            ModelBalls.Clear();
            _logicApi.LogicBalls.Clear();

            _logicApi.StartMovement(numberOfBalls);
            var logicBalls = _logicApi.LogicBalls;

            foreach (var ball in logicBalls)
            {
                ModelBall modelBall = new(ball.Position.X, ball.Position.Y);
                ILogicBall logicBall = ball;
                logicBall.Subscribe(modelBall);
                ModelBalls.Add(modelBall);
            }
        }

        public void Stop()
        {
            ModelBalls.Clear();
            _logicApi.StopMovement();
        }

       
    }
}
