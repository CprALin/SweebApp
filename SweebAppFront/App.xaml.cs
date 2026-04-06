using SweebAppFront.Services;
using SweebAppFront.Services.Interfaces;
using SweebAppFront.Views;

namespace SweebAppFront
{
    public partial class App : Application
    {
        private readonly MainWindow _mainWindow;
        private readonly LoginPage _loginPage;
        private readonly MainPage _mainPage;
        private readonly IAuthStateService _authStateService;

        public App(MainWindow mainWindow, LoginPage loginPage, MainPage mainPage, IAuthStateService authStateService)
        {
            InitializeComponent();

            _mainWindow = mainWindow;
            _loginPage = loginPage;
            _mainPage = mainPage;
            _authStateService = authStateService;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            _mainWindow.Page = _authStateService.IsLoggedIn
                ? _mainPage
                : _loginPage;

            _mainWindow.Width = 1200;
            _mainWindow.Height = 600;

            _mainWindow.MinimumWidth = 1200;
            _mainWindow.MinimumHeight = 600;
            
            return _mainWindow;
        }
    }
}