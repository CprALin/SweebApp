using SweebAppFront.Views;

namespace SweebAppFront
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new MainPage());
        }
    }
}