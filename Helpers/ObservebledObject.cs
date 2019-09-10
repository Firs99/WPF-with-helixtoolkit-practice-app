﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace _3DHelixToolKitApp.Helpers
{
    public class ObservebledObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
