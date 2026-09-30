using Microsoft.EntityFrameworkCore;
using SimpleResilientPaymentScenario.api.Domain.Models;

namespace SimpleResilientPaymentScenario.api.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Payment>(entity =>
        {
            entity.ToTable("Payment");
            entity.HasKey(x => x.PaymentId);
            entity.Property(x => x.OrderId).IsRequired().HasMaxLength(100);
            entity.Property(x => x.CustomerId).IsRequired();
            entity.Property(x => x.Amount).IsRequired();
            entity.Property(x => x.Status).IsRequired();
            entity.Property(x => x.CreatedAt).IsRequired();
        });
    }
}