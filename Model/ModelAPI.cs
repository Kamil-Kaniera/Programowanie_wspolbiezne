using Logic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Model
{
    public class ModelAPI : IModelAPI
    {
        private readonly ILogicAPI logicAPI;

        public readonly ObservableCollection<BallModel> ModelBalls = new();
        public event PropertyChangedEventHandler PropertyChanged;

        public ModelAPI()
        {
            logicAPI = ILogicAPI.createAPI();
        }

        public ModelAPI(ILogicAPI logicAPI)
        {
            this.logicAPI = logicAPI;
        }

        public void ClearBalls()
        {
            logicAPI.StopMovement();
            ModelBalls.Clear();
        }

        public void GetCircles()
        {
            foreach (BallLogic s in logicAPI.GetBalls()) 
                ModelBalls.Add(new BallModel(s));
        }

        public void Start(int ballsAmount)
        {
            ModelBalls.Clear();
            logicAPI.CreateBalls(ballsAmount);
            logicAPI.StartMovement();
            GetCircles();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}