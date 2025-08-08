using MauiCustomerApp.Models;
using MauiCustomerApp.Services;

namespace MauiCustomerApp.Views;

public partial class EditCustomerPage : ContentPage
{
    private readonly CustomerService _service;
    private Customer _customer;
    public event EventHandler? CustomerUpdated;

    public EditCustomerPage(CustomerService service, Customer selectedCustomer)
    {
        InitializeComponent();
        _service = service;
        _customer = selectedCustomer ?? throw new ArgumentNullException(nameof(selectedCustomer));

        // Carrega os dados do cliente nos campos
        LoadCustomerData();
    }

    private void LoadCustomerData()
    {
        NameEntry.Text = _customer.Name;
        LastnameEntry.Text = _customer.Lastname;
        AgeEntry.Text = _customer.Age.ToString();
        AddressEntry.Text = _customer.Address;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameEntry.Text) ||
            string.IsNullOrWhiteSpace(LastnameEntry.Text) ||
            !int.TryParse(AgeEntry.Text, out int age) ||
            string.IsNullOrWhiteSpace(AddressEntry.Text))
        {
            await DisplayAlert("Error", "Please fill all fields correctly.", "OK");
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
}
