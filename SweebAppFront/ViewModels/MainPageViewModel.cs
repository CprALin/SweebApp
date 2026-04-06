using SweebAppFront.Services.Interfaces;
using System.Windows.Input;

namespace SweebAppFront.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        private View? _currentView;
        private string _currentPageKey = "Dashboard";

        public View? CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        public string CurrentPageKey
        {
            get => _currentPageKey;
            set => SetProperty(ref _currentPageKey, value);
        }

        public ICommand NavigateCommand { get; }

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            NavigateCommand = new Command<string>(Navigate);
            Navigate(CurrentPageKey);
        }

        private void Navigate(string key)
        {
            CurrentPageKey = key;
            CurrentView = _navigationService.GetView(key);
        }
    }
}
