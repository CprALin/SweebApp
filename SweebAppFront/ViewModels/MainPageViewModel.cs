using SweebAppFront.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SweebAppFront.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private View? _currentView;
        private string _currentPageKey = "Dashboard";

        public event PropertyChangedEventHandler? PropertyChanged;

        public View? CurrentView
        {
            get => _currentView;
            set
            {
                if (_currentView == value) return;
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public string CurrentPageKey
        {
            get => _currentPageKey;
            set
            {
                if (_currentPageKey == value) return;
                _currentPageKey = value;
                OnPropertyChanged();
            }
        }

        public ICommand NavigateCommand { get; }

        public MainPageViewModel()
        {
            NavigateCommand = new Command<string>(Navigate);
            Navigate(CurrentPageKey);
        }
        private void Navigate(string key)
        {
            CurrentPageKey = key;

            CurrentView = key switch
            {
                "Dashboard" => new DashboardView(),
                "LiveRequests" => new LiveRequestsView(),
                "Devices" => new DevicesView(),
                "Rules" => new RulesView(),
                "Threats" => new ThreatsView(),
                _ => new DashboardView()
            };
        }

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
