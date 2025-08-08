namespace MauiCustomerApp.Models;

public class Customer
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Lastname { get; set; }
    public int Age { get; set; }
    public required string Address { get; set; }
}
