using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Data { 
    public class Ball
    {
        private int _x;
        private int _y;
        
        public  Ball(int x, int y) {
             _x = x;
             _y = y;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        
        public int PositionX {
            get => _x;
            set {
                _x = value;
                OnPropertyChanged();
            }
        }

        public int PositionY {
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
