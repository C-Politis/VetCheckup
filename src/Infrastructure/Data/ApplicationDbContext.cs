using Microsoft.EntityFrameworkCore;

namespace VetCheckup.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext() { }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    #region Methods

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyUtility.GetAssembly());

        base.OnModelCreating(modelBuilder);
    }

    #endregion

}
