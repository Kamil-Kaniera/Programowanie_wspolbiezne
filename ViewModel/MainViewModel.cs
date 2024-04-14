using System.Collections.ObjectModel;
using System.Windows.Input;
using Model;


namespace ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand StartCommand { get; set; }
        public ICommand StopCommand { get; set; }

        private int _count;
        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                OnPropertyChanged();
            }
        }

        private ModelAPI modelAPI { get; set; }

        public ObservableCollection<BallModel> Balls
        {
            get { return modelAPI.ModelBalls; }
        }

        public MainViewModel()
        {
            modelAPI = new ModelAPI();
            StartCommand = new Commands(Start);
            StopCommand = new Commands(Stop);
        }

        private void Start()
        {
            Balls.Clear();
            modelAPI.Start(Count);
        }

        private void Stop()
        {
            modelAPI.ClearBalls();
        }
    }
    
}
