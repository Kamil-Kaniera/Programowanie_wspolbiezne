using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal interface IBallRepository // AKA Data API
    {
        void AddBall(Ball ball);
        void RemoveBall(Ball ball);
        void RemoveAllBalls();
        List<Ball> GetBalls();
    }
}
