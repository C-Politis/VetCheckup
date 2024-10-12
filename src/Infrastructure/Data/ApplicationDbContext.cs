using Microsoft.EntityFrameworkCore;
using VetCheckup.Application.Services.Persistence;

namespace VetCheckup.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IDbContext
{
    #region Constructors

    public ApplicationDbContext() { }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    #endregion

    #region DbContext Methods

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

    #region IDbContext Implementation

    void IDbContext.Add<TEntity>(TEntity entity)
    {
        if (base.Model.FindEntityType(nameof(TEntity)) == null)
            throw new NotSupportedException($"{nameof(TEntity)} is not currently tracked in the DbContext Model");

        base.Add(entity);
    }

    IQueryable<TEntity> IDbContext.Get<TEntity>() where TEntity : class
    {
        if (base.Model.FindEntityType(nameof(TEntity)) == null)
            throw new NotSupportedException($"{nameof(TEntity)} is not currently tracked in the DbContext Model");

        return base.Set<TEntity>();
    }

    void IDbContext.Remove<TEntity>(TEntity entity)
    {
        if (base.Model.FindEntityType(nameof(TEntity)) == null)
            throw new NotSupportedException($"{nameof(TEntity)} is not currently tracked in the DbContext Model");

        base.Remove(entity);
    }

    async Task IDbContext.SaveChangesAsync(CancellationToken cancellationToken)
    {
        try
        {
            using var _Transaction = await this.Database.BeginTransactionAsync(cancellationToken);
            {
                await base.SaveChangesAsync(cancellationToken);
            }
        }
        catch
        {
            await this.Database.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    #endregion

}
