using MauiCustomerApp.Models;
using MauiCustomerApp.Services;
using MauiCustomerApp.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MauiCustomerApp.ViewModels;

public class MainViewModel : BindableObject
{
    #region Variables
    private readonly CustomerService _service;
    private Customer? _selectedCustomer;
    #endregion

    #region Props
    public ObservableCollection<Customer> Customers { get; set; } = new();

    public Customer? SelectedCustomer
    {
        get => _selectedCustomer;
        set
        {
            _selectedCustomer = value;
            OnPropertyChanged();
        }
    }
    #endregion

    #region Command
    public ICommand LoadCommand { get; }
    public ICommand AddCommand { get; }
    public ICommand EditCommand { get; }
    public ICommand DeleteCommand { get; }
    #endregion

    #region Constructor
    public MainViewModel(CustomerService service)
    {
        _service = service;

        LoadCommand = new Command(async () => await LoadAsync());
        AddCommand = new Command(async () => await OnAdd());
        EditCommand = new Command(async () => await OnEdit());
        DeleteCommand = new Command(async () => await OnDelete());

        Task.Run(LoadAsync);
    }
    #endregion

    #region Methods
    public async Task LoadAsync()
    {
        var list = await _service.GetAllAsync();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Customers.Clear();
            foreach (var item in list)
                Customers.Add(item);
        });
    }

    private async Task OnAdd()
    {
        var mainPage = Application.Current?.Windows.FirstOrDefault()?.Page;
        if (mainPage?.Navigation != null)
        {
            var addPage = new AddCustomerPage(_service);
            addPage.CustomerAdded += async (s, e) => await LoadAsync();

            // Configurar janela modal centralizada
            ConfigureModalWindow(addPage, "Adicionar Cliente", 500, 700);

            await mainPage.Navigation.PushModalAsync(addPage);
        }
    }

    private async Task OnEdit()
    {
        if (SelectedCustomer == null) return;

        var mainPage = Application.Current?.Windows.FirstOrDefault()?.Page;
        if (mainPage?.Navigation != null)
        {
            var editPage = new EditCustomerPage(_service, SelectedCustomer);
            editPage.CustomerUpdated += async (s, e) => await LoadAsync();

            // Configurar janela modal centralizada
            ConfigureModalWindow(editPage, "Editar Cliente", 500, 700);

            await mainPage.Navigation.PushModalAsync(editPage);
        }
    }

    private void ConfigureModalWindow(ContentPage page, string title, double width, double height)
    {
        page.Title = title;

#if WINDOWS
        page.Loaded += (s, e) =>
        {
            if (page.Handler?.PlatformView is Microsoft.UI.Xaml.FrameworkElement element)
            {
                if (element.XamlRoot?.Content is Microsoft.UI.Xaml.FrameworkElement rootElement)
                {
                    // Centralizar a janela modal
                    element.Width = width;
                    element.Height = height;
                    element.HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Center;
                    element.VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Center;
                }
            }
        };
#endif
    }

    private async Task OnDelete()
    {
        if (SelectedCustomer == null) return;

        var mainPage = Application.Current?.Windows.FirstOrDefault()?.Page;
        if (mainPage != null)
        {
            bool confirm = await mainPage.DisplayAlert(
                "Excluir Cadastro", $"Excluir {SelectedCustomer.Name}?", "Sim", "Não");

            if (confirm)
            {
                await _service.DeleteAsync(SelectedCustomer);
                await LoadAsync();
            }
        }
    }
    #endregion
}
