﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    public class LogicAPI(IDataAPI dataAPI) : ILogicAPI
    {
        private readonly Table _table = new(450, 450);

        private DataAPI api = (DataAPI)dataAPI;

        private List<Task> _tasks = [];

        private bool _movement = false;



        public void CreateBalls(int numberOfBalls)
        {
            api.RemoveAllBalls();
            for (int i = 0; i < numberOfBalls; i++)
            {
                // Randomize starting position
                Random rnd = new();
                int randomizedX = rnd.Next(0, _table.SizeY);
                int randomizedY = rnd.Next(0, _table.SizeX);

                Ball ball = new(randomizedX, randomizedY);

                api.AddBall(ball);

                // Add new task for new ball
                _tasks.Add(new Task(() =>
                {
                    int direction = rnd.Next(0, 4);
                    while (_movement)
                    {
                        switch (direction)
                        {
                            case 0: // UP
                                MoveBall(ball, 0, 1);
                                break;
                            case 1: // DOWN
                                MoveBall(ball, 0, -1);
                                break;
                            case 2: // RIGHT
                                MoveBall(ball, 1, 0);
                                break;
                            case 3: // LEFT
                                MoveBall(ball, -1, 0);
                                break;
                        }

                        Thread.Sleep(100);
                    }
                }));
            }

        }

        public void MoveBall(Ball ball, int x, int y)
        {
            int newX = ball.X + x;
            int newY = ball.Y + y;
            if (CheckWallCollision(newX, newY))
            {
                ball.X += x;
                ball.Y += y;
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

        public List<BallLogic> GetBalls()
        {
            List<BallLogic> balls = [];

            foreach (Ball b in api.GetBalls())
            {
                balls.Add(new BallLogic(b));
            }

            return balls;
        }

        public void StartMovement()
        {
            _movement = true;

            foreach (Task task in _tasks)
            {
                if (task.Status == TaskStatus.Created)
                {
                    task.Start();
                }
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

                api.RemoveAllBalls();
                _tasks.Clear();
            }
        }

        public List<List<int>> GetAllBallsPosition()
        {
            List<List<int>> result = [];

            foreach (Ball ball in api.GetBalls())
            {
                List<int> position = new List<int>
                {
                    ball.X, ball.Y
                };
                result.Add(position);
            }

            return result;
        }
    }
}
