using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;

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
            var page = new AppShell();

            var titleBar = new TitleBar
            {
                Title = "",
                Subtitle = "",
                BackgroundColor = Color.FromRgba("#1E293B"),
                ForegroundColor = Colors.White
            };

            var appTitle = new Label
            {
                Text = "SweebApp",
                TextColor = Colors.White,
                VerticalOptions = LayoutOptions.Center
            };

            var minBtn = new Button { Text = "_", WidthRequest = 40 };
            var maxBtn = new Button { Text = "□", WidthRequest = 40 };
            var closeBtn = new Button { Text = "X", WidthRequest = 40, BackgroundColor = Colors.Red };

            minBtn.Clicked += (_, _) => WindowCommands.Minimize();
            maxBtn.Clicked += (_, _) => WindowCommands.ToggleMaximize();
            closeBtn.Clicked += (_, _) => WindowCommands.Close();

            titleBar.Content = appTitle;
            titleBar.TrailingContent = new HorizontalStackLayout
            {
                Spacing = 6,
                Children = {minBtn ,maxBtn, closeBtn}
            };

            titleBar.PassthroughElements.Add(minBtn);
            titleBar.PassthroughElements.Add(maxBtn);
            titleBar.PassthroughElements.Add(closeBtn);

            var window = new Window(page)
            {
                TitleBar = titleBar
            };

            return window;     
        }
    }
}