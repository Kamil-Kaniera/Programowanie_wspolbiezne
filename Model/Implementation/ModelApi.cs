using System.Collections.ObjectModel;
using Logic.Abstract;
using Logic.Implementation;
using Model.Abstract;

namespace Model.Implementation
{
    public class ModelApi : IModelApi
    {
        private readonly ILogicApi _logicApi;
        public ObservableCollection<IModelBall> ModelBalls { get; } = [];

        public ModelApi()
        {
            _logicApi = new LogicApi();
        }

        public ModelApi(ILogicApi logic)
        {
            _logicApi = logic;
        }

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
