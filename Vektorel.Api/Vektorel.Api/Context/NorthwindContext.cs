using Microsoft.EntityFrameworkCore;
using Vektorel.Api.Entities;

namespace Vektorel.Api.Context;

public class NorthwindContext : DbContext
{
    public NorthwindContext(DbContextOptions<NorthwindContext> dbContextOptions) : base(dbContextOptions)
    {
        
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
}
