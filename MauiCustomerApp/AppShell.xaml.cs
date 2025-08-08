using MauiCustomerApp.Views;

namespace MauiCustomerApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Registro de rotas para navegação
            Routing.RegisterRoute(nameof(AddCustomerPage), typeof(AddCustomerPage));
            Routing.RegisterRoute(nameof(EditCustomerPage), typeof(EditCustomerPage));
        }
    }
}
