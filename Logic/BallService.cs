using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    public class BallService(int tableX, int tableY) : IBallService
    {
        private readonly Table _table = new(tableX, tableY);
        private BallRepository _repository = new();
        private List<Task> _tasks = [];
        private bool _movement = false;

        public void CreateBalls(int numberOfBalls)
        {
            for (int i = 0; i < numberOfBalls; i++)
            {
                // Randomize starting position
                Random rnd = new();
                int randomizedX = rnd.Next(0, _table.SizeY);
                int randomizedY = rnd.Next(0, _table.SizeX);

                Ball ball = new(randomizedX, randomizedY);

                _repository.AddBall(ball);

                // Add new task for new ball
                _tasks.Add(new Task(() =>
                {
                    while (_movement)
                    {
                        //Randomize movement - <-1;1> move in both directions
                        int moveX = rnd.Next(0, 2) - 1;
                        int moveY = rnd.Next(0, 2) - 1;
                        MoveBall(ball, moveX, moveY);
                        Thread.Sleep(100);
                    }
                }));
            }

        }

        public void MoveBall(Ball ball, int x, int y)
        {
            int newX = ball.positionX + x;
            int newY = ball.positionY + y;
            if (CheckWallCollision(newX, newY))
            {
                ball.positionX += x;
                ball.positionY += y;
            }
        }

        public bool CheckWallCollision(int x, int y)
        {
            if (x > 0 && x < _table.SizeX && y > 0 && y < _table.SizeY)
            {
                return true;
            }
            else return false;
        }

        public List<Ball> GetBalls()
        {
            return _repository.GetBalls();
        }

        public void StartMovement()
        {
            _movement = true;

            foreach (Task task in _tasks)
            {
                task.Start();
            }
        }

        public void StopMovement()
        {
            _movement = false;

            // Check if every task is completed 
            bool IsEveryTaskCompleted = false;

            while (!IsEveryTaskCompleted)
            {
                IsEveryTaskCompleted = true;
                foreach (Task task in _tasks)
                {
                    if (!task.IsCompleted)
                    {
                        IsEveryTaskCompleted = false;
                        break;
                    }
                }
            }

            // If every task is completed clear all tasks and remove all balls
            if (IsEveryTaskCompleted)
            {
                foreach (Task task in _tasks)
                {
                    task.Dispose();
                }

                _repository.RemoveAllBalls();
                _tasks.Clear();
            }
        }

        public List<List<int>> GetAllBallsPosition()
        {
            List<List<int>> result = [];

            foreach (Ball ball in _repository.GetBalls())
            {
                List<int> position = new List<int>
                {
                    ball.positionX, ball.positionY
                };
                result.Add(position);
            }

            return result;
        }
    }
}
