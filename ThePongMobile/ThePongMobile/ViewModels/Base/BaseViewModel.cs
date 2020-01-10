using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ThePongMobile.Services;

namespace ThePongMobile.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public abstract Type PageType { get; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetValue<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
            {
                return;
            }
            backingField = value;
            OnPropertyChanged(propertyName);
        }

        
    }
}
        
           

