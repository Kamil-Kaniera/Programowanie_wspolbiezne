using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Model.Abstract;
using Model.Implementation;
using Logic.Implementation;
using Data.Implementation;

namespace ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IModelApi _modelApi;
        private int _count = 5;

        public bool IsStarted { get; set; }

         private ObservableCollection<IModelBall> _balls;

        public MainViewModel()
        {
            _modelApi = DependencyInjection.Get<IModelApi>();
            _balls = _modelApi.ModelBalls;
            StartCommand = new(Start, () => !GetIsStarted());
            StopCommand = new(Stop, GetIsStarted);
        }

        public Commands StartCommand { get; set; }
        public Commands StopCommand { get; set; }

        public ObservableCollection<IModelBall> Balls
        {
            get => _balls;
            set
            {
                _balls = value;
                OnPropertyChanged(nameof(Balls));
            }
        }

        public int Count
        {
            get => _count;
            set => SetField(ref _count, value);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private bool GetIsStarted() => IsStarted;

        private void ToggleIsStarted()
        {
            IsStarted = !IsStarted;
            StartCommand?.OnCanExecuteChanged();
            StopCommand?.OnCanExecuteChanged();
        }
        private void Start()
        {
            Balls.Clear();
            _modelApi.Start(Count);
            ToggleIsStarted();
        }

        private void Stop()
        {
            _modelApi.Stop();
            ToggleIsStarted();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
