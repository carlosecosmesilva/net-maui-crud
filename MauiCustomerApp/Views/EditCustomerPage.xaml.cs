using MauiCustomerApp.Models;
using MauiCustomerApp.Services;
using Windows.UI.Popups;

namespace MauiCustomerApp.Views;

public partial class EditCustomerPage : ContentPage
{
    #region Variables
    private readonly CustomerService _service;
    private Customer _customer;
    public event EventHandler? CustomerUpdated;
    #endregion

    #region Constructor
    public EditCustomerPage(CustomerService service, Customer selectedCustomer)
    {
        InitializeComponent();
        _service = service;
        _customer = selectedCustomer ?? throw new ArgumentNullException(nameof(selectedCustomer));

        // Carrega os dados do cliente nos campos
        LoadCustomerData();
    }
    #endregion

    #region Methods
    private void LoadCustomerData()
    {
        NameEntry.Text = _customer.Name;
        LastnameEntry.Text = _customer.Lastname;
        AgeEntry.Text = _customer.Age.ToString();
        AddressEntry.Text = _customer.Address;
    }

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

        _customer.Name = NameEntry.Text;
        _customer.Lastname = LastnameEntry.Text;
        _customer.Age = age;
        _customer.Address = AddressEntry.Text;

        await _service.UpdateAsync(_customer);
        CustomerUpdated?.Invoke(this, EventArgs.Empty);
        await Navigation.PopModalAsync();
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
    #endregion
}
