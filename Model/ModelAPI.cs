using Logic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Model
{
    public class ModelAPI(ILogicAPI logicAPI) : IModelAPI
    {
        private LogicAPI api = (LogicAPI)logicAPI;

        public event PropertyChangedEventHandler PropertyChanged;

        public void ClearBalls()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<BallModel> GetCircles()
        {
            throw new NotImplementedException();
        }

        public void Start(int BallsAmount, int Radius)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
