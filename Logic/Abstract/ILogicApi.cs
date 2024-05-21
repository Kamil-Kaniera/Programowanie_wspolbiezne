using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Abstract;

namespace Logic.Abstract
{
    public interface ILogicApi : IObserver<IBall>
    {
        void StartMovement(int numberOfBalls);
        void StopMovement();
        List<ILogicBall> LogicBalls { get; }
    }
}
