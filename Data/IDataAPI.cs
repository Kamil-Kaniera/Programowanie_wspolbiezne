using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IDataAPI 
    {
        static IDataAPI cerateAPI() => new DataAPI();
        void AddBall(Ball ball);
        void RemoveAllBalls();
        List<Ball> GetBalls();
    }
}
