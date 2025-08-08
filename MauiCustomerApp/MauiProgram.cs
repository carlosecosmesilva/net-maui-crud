using MauiCustomerApp.Data;
using MauiCustomerApp.Services;
using MauiCustomerApp.ViewModels;
using MauiCustomerApp.Views;
using Microsoft.EntityFrameworkCore;
using CommunityToolkit.Maui;

namespace MauiCustomerApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        //String de conexão PostgreSQL
        var connectionString = "Host=localhost;Port=5432;Database=maui_customer_app;Username=postgres;Password=postgres";

        //Registra o DbContext com PostgreSQL
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        //Registra Services
        builder.Services.AddScoped<CustomerService>();

        //Registra ViewModels
        builder.Services.AddSingleton<MainViewModel>();

        //Registra Views (páginas)
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<AddCustomerPage>();
        builder.Services.AddTransient<EditCustomerPage>();

        var app = builder.Build();

        // Aplica migrações automaticamente na inicialização
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Database.Migrate();
        }

        return app;
    }
}