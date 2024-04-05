using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal interface IBallService
    {
        void createBalls(int numberOfBalls);
        void moveBall();
    }
}
