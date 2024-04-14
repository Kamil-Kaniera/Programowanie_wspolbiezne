using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Data { 
    public class Ball : INotifyPropertyChanged
    {
        private int _x;
        private int _y;
        
        public  Ball(int x, int y) {
             _x = x;
             _y = y;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        
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
        
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }        
    }
}
