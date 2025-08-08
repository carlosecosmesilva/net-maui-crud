using MauiCustomerApp.Models;
using MauiCustomerApp.Services;

namespace MauiCustomerApp.Views;

public partial class AddCustomerPage : ContentPage
{
    private readonly CustomerService _service;
    public event EventHandler? CustomerAdded;

    public AddCustomerPage(CustomerService service)
    {
        InitializeComponent();
        _service = service;
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
}
