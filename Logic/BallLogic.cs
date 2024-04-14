using System.ComponentModel;
using System.Runtime.CompilerServices;
using Data;

namespace Logic
{
    public class BallLogic : INotifyPropertyChanged
    {
        private int _x;
        private int _y;

        public BallLogic(Ball s) {
            X = s.X;
            Y = s.X;
            Random random = new Random();
            s.PropertyChanged += UpdateBall;
        }

        private void UpdateBall(object sender, PropertyChangedEventArgs e) {
            
            Ball ball = (Ball)sender;
            if (e.PropertyName == "X")
            {
                X = ball.X;
            }
            else if (e.PropertyName == "Y")
            {
                Y = ball.Y;
            }

        }
        
        public int X {
            get => _x;
            set {
                _x = value;
                OnPropertyChanged();
            }
        }

        public int Y {
            get => _y;
            set {
                _y = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}