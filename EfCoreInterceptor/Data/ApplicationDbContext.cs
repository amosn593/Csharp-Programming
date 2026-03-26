using EfCoreInterceptor.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreInterceptor.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Invoice> Invoices => Set<Invoice>();

}
