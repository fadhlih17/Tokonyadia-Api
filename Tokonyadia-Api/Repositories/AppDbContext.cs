using Microsoft.EntityFrameworkCore;
using Tokonyadia_Api.Entities;

namespace Tokonyadia_Api.Repositories;

public class AppDbContext : DbContext
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductPrice> ProductPrices => Set<ProductPrice>();
    public DbSet<Purchase> Purchases => Set<Purchase>();
    public DbSet<PurchaseDetail> PurchaseDetails => Set<PurchaseDetail>();
    public DbSet<Store> Stores => Set<Store>();
    public DbSet<UserCredential> UserCredentials => Set<UserCredential>();
    public DbSet<Role> Roles => Set<Role>();

    protected AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasIndex(customer => customer.PhoneNumber).IsUnique();
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasIndex(store => store.SiupNumber).IsUnique();
            entity.HasIndex(store => store.PhoneNumber).IsUnique();
        });

        modelBuilder.Entity<UserCredential>(entity =>
        {
            entity.HasIndex(credential => credential.Email).IsUnique();
        });
    }
}