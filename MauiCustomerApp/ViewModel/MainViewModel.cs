using MauiCustomerApp.Models;
using MauiCustomerApp.Services;
using MauiCustomerApp.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MauiCustomerApp.ViewModels;

public class MainViewModel : BindableObject
{
    #region Variaveis
    private readonly CustomerService _service;
    private Customer? _selectedCustomer;
    #endregion

    #region Propriedades
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

    #region Comandos
    public ICommand LoadCommand { get; }
    public ICommand AddCommand { get; }
    public ICommand EditCommand { get; }
    public ICommand DeleteCommand { get; }
    #endregion

    #region Construtor
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

    #region Métodos
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
            await mainPage.Navigation.PushModalAsync(editPage);
        }
    }

    private async Task OnDelete()
    {
        if (SelectedCustomer == null) return;

        var mainPage = Application.Current?.Windows.FirstOrDefault()?.Page;
        if (mainPage != null)
        {
            bool confirm = await mainPage.DisplayAlert(
                "Confirm", $"Delete {SelectedCustomer.Name}?", "Yes", "No");

            if (confirm)
            {
                await _service.DeleteAsync(SelectedCustomer);
                await LoadAsync();
            }
        }
    }
    #endregion
}
