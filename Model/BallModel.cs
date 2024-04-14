using System.ComponentModel;
using System.Runtime.CompilerServices;
using Logic;

namespace Model
{

    public class BallModel : INotifyPropertyChanged
    {
        public int X
        {
            get => _x;
            set
            {
                _x = value;
                OnPropertyChanged();
            }
        }

        public int Y
        {
            get => _y;
            set
            {
                _y = value;
                OnPropertyChanged();
            }
        }
        

        private int _x { get; set; }
        private int _y { get; set; }

        public BallModel(BallLogic s) {
            X = s.X;
            Y = s.Y;
            s.PropertyChanged += UpdateBall;
        }

        public event PropertyChangedEventHandler? PropertyChanged;


        public void UpdateBall(Object sender, PropertyChangedEventArgs e)
        {
            BallLogic ball = (BallLogic)sender;
            if (e.PropertyName == "X")
            {
                X = ball.X;
            }
            else if (e.PropertyName == "Y")
            {
                Y = ball.Y;
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}