using MauiCustomerApp.Data;
using MauiCustomerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MauiCustomerApp.Services;

public class CustomerService
{
    #region Variables
    private readonly AppDbContext _context;
    #endregion

    #region Constructor
    public CustomerService(AppDbContext context)
    {
        _context = context;
    }
    #endregion

    #region Methods
    public async Task<List<Customer>> GetAllAsync()
    {
        return await _context.Customers
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    public async Task AddAsync(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Customer customer)
    {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Customer customer)
    {
        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }
    #endregion
}
