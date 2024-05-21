using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commons;

namespace Data.Abstract
{
    public interface IBall : IObservable<IBall>, IDisposable
    {
        Position Position { get; }
        VelocityVector Velocity { get; set; }
        int Diameter { get; }
    }
}
