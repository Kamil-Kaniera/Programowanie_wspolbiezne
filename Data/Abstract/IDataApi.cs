using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Implementation;

namespace Data.Abstract
{
    public interface IDataApi
    {
        void AddBall(Ball ball);
        void RemoveAllBalls();
        Table GetTable();
        List<IBall> Balls { get; }
    }
}
