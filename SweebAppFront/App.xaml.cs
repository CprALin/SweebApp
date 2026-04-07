using SweebAppFront.Services;
using SweebAppFront.Services.Interfaces;
using SweebAppFront.Views;
using System.ComponentModel;

namespace SweebAppFront
{
    public partial class App : Application
    {
        private readonly MainWindow _mainWindow;
        private readonly LoginPage _loginPage;
        private readonly MainPage _mainPage;
        private readonly IAuthStateService _authStateService;
        private readonly IServiceProvider _serviceProvider;

        public App(MainWindow mainWindow, LoginPage loginPage, MainPage mainPage, IAuthStateService authStateService, IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _mainWindow = mainWindow;
            _loginPage = loginPage;
            _mainPage = mainPage;
            _authStateService = authStateService;
            _serviceProvider = serviceProvider;

            if(_authStateService is INotifyPropertyChanged notify)
            {
                notify.PropertyChanged += OnAuthStateChanged;
            }
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            SetRootPage();

            _mainWindow.Width = 1200;
            _mainWindow.Height = 600;

            _mainWindow.MinimumWidth = 1200;
            _mainWindow.MinimumHeight = 600;
            
            return _mainWindow;
        }

        private void OnAuthStateChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(IAuthStateService.IsLoggedIn))
            {
                SetRootPage();
            }
        }

        private void SetRootPage()
        {
           if(_authStateService.IsLoggedIn)
           {
                _mainWindow.Page = _serviceProvider.GetRequiredService<MainPage>();
            }
            else
            {
                _mainWindow.Page = _serviceProvider.GetRequiredService<LoginPage>();
            }
        }
    }
}