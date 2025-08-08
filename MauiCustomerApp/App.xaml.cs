namespace MauiCustomerApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = new Window(new AppShell())
            {
                Title = "Sistema de Gerenciamento de Clientes",
                MinimumWidth = 800,
                MinimumHeight = 600
            };

            // Configurar janela principal maximizada
#if WINDOWS
            window.Created += (s, e) =>
            {
                var platformWindow = window.Handler?.PlatformView;
                if (platformWindow is Microsoft.UI.Xaml.Window winUIWindow)
                {
                    var presenter = winUIWindow.AppWindow.Presenter as Microsoft.UI.Windowing.OverlappedPresenter;
                    if (presenter != null)
                    {
                        presenter.Maximize();
                    }
                }
            };
#endif

            return window;
        }
    }
}