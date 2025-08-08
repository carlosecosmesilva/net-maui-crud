using MauiCustomerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MauiCustomerApp.Data;

public class AppDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Lastname).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Age).IsRequired();
            entity.Property(e => e.Address).HasMaxLength(200);

            // Dados de exemplo (seed data)
            entity.HasData(
                new Customer { Id = 1, Name = "João", Lastname = "Silva", Age = 30, Address = "Rua das Flores, 123" },
                new Customer { Id = 2, Name = "Maria", Lastname = "Santos", Age = 25, Address = "Av. Brasil, 456" },
                new Customer { Id = 3, Name = "Pedro", Lastname = "Oliveira", Age = 35, Address = "Rua da Paz, 789" },
                new Customer { Id = 4, Name = "Ana", Lastname = "Costa", Age = 28, Address = "Av. Central, 321" },
                new Customer { Id = 5, Name = "Carlos", Lastname = "Ferreira", Age = 42, Address = "Rua do Comércio, 654" }
            );
        });
    }
}
