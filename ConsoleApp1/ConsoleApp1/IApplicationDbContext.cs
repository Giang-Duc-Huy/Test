using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1
{
    public interface IApplicationDbContext
    {
        DbSet<Order> Orders { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<OrderItem> OrderItems { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
