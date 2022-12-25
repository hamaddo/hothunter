using Microsoft.EntityFrameworkCore;

namespace Rgr.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<JobRequest> JobRequests { get; set; }
    public DbSet<Employer> Employers { get; set; }
    public DbSet<Offer> Offers { get; set; }
}