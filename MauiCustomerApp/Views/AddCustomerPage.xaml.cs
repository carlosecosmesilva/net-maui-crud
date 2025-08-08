using MauiCustomerApp.Models;
using MauiCustomerApp.Services;

namespace MauiCustomerApp.Views;

public partial class AddCustomerPage : ContentPage
{
    #region Variables
    private readonly CustomerService _service;
    public event EventHandler? CustomerAdded;
    #endregion

    #region Constructor
    public AddCustomerPage(CustomerService service)
    {
        InitializeComponent();
        _service = service;
    }
    #endregion

    #region Methods
    private bool TryValidateCustomerInput(out int age, out string errorMessage)
    {
        errorMessage = string.Empty;
        age = 0;

        if (string.IsNullOrWhiteSpace(NameEntry.Text))
        {
            errorMessage = "O nome é obrigatório.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(LastnameEntry.Text))
        {
            errorMessage = "O endereço é obrigatório.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(AddressEntry.Text))
        {
            errorMessage = "O endereço é obrigatório.";
            return false;
        }

        if (!int.TryParse(AgeEntry.Text, out age))
        {
            errorMessage = "A idade deve ser um número válido.";
            return false;
        }

        return true;
    }
    #endregion

    #region Events
    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (!TryValidateCustomerInput(out int age, out string errorMessage))
        {
            await DisplayAlert("Erro", errorMessage, "OK");
            return;
        }

        var customer = new Customer
        {
            Name = NameEntry.Text,
            Lastname = LastnameEntry.Text,
            Age = age,
            Address = AddressEntry.Text
        };

        await _service.AddAsync(customer);
        CustomerAdded?.Invoke(this, EventArgs.Empty);
        await Navigation.PopModalAsync();
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
    #endregion
}
