using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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

        optionsBuilder.UseSqlServer(@"Server=(localdb)\\mssqllocaldb;Database=VetCheckupDb;
                    Trusted_Connection=True;
                    MultipleActiveResultSets=true");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyUtility.GetAssembly());

        base.OnModelCreating(modelBuilder);
    }

    #endregion

}
