using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    internal interface IBallService
    {
        void createBalls(int numberOfBalls);
        void moveBall(Ball ball, int x, int y);
    }
}
