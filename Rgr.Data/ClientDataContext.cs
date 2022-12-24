using Microsoft.EntityFrameworkCore;

namespace Rgr.Data;

public class ClientDataContext: DbContext
{
    public ClientDataContext(DbContextOptions<ClientDataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<JobRequest> JobRequests { get; set; }
}