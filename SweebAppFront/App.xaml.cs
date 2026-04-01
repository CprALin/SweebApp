using SweebAppFront.Views;

namespace SweebAppFront
{
    public partial class App : Application
    {
        public MainWindow MainWindow { get; }
        public App(MainWindow mainWindow)
        {
            InitializeComponent();
            MainWindow = mainWindow;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = new MainWindow
            {
                Page = new MainPage()
            };

            window.Width = 1200;
            window.Height = 600;

            window.MinimumWidth = 1200;
            window.MinimumHeight = 600;
            
            return window;
        }
    }
}