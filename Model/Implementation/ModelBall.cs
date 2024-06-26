﻿using Logic.Abstract;
using Model.Abstract;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model.Implementation
{
    public class ModelBall : IModelBall
    {
        private const int Rescale = 100;

        private int _x;
        private int _y;
        public int Diameter { get; } = 20 * Rescale;

        public ModelBall(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X
        {
            get => _x;
            set
            {
                _x = value;
                RaisePropertyChanged(nameof(X));
            }
        }

        public int Y
        {
            get => _y;
            set
            {
                _y = value;
                RaisePropertyChanged(nameof(Y));
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnCompleted() {}

        public void OnError(Exception error) {}

        public void OnNext(ILogicBall value)
        {
            X = value.Position.X;
            Y = value.Position.Y;
        }

        internal void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        
    }
}
