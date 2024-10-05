using Microsoft.EntityFrameworkCore;

namespace VetCheckup.Infrastructure.Data;
public class ApplicationDbContext : DbContext
{

    #region Methods

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyUtility.GetAssembly());

        base.OnModelCreating(modelBuilder);
    }

    #endregion

}
