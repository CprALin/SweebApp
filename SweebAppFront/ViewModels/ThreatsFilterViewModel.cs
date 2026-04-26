using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SweebAppFront.ViewModels
{
    public class ThreatsFilterViewModel : BaseViewModel
    {
        private string _currentFilterKey = "All";

        public string CurrentFilterKey
        {
            get => _currentFilterKey;
            set => SetProperty(ref _currentFilterKey, value);
        }

        public ICommand FilterCommand { get; }

        public ThreatsFilterViewModel()
        {
            FilterCommand = new Command<string>(SetFilter);
            SetFilter(CurrentFilterKey);
        }

        private void SetFilter(string key)
        {
            CurrentFilterKey = key;
        }
    }
}
