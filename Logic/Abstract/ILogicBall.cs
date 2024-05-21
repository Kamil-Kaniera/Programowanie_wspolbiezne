using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commons;
using Data.Abstract;

namespace Logic.Abstract
{
    public interface ILogicBall : IObserver<IBall>, IObservable<ILogicBall>
    {
       Position Position { get; }
       int Diameter { get; }
    }
}
