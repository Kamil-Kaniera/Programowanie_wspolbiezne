using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    internal interface IBallService // AKA Logic API
    {
        void CreateBalls(int numberOfBalls);
        void MoveBall(Ball ball, int x, int y);
        List<Ball> GetBalls();
        List<List<int>> GetAllBallsPosition();
        bool CheckWallCollision(int x, int y);
        void StartMovement();
        void StopMovement();
    }
}
