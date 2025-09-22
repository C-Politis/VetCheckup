using Microsoft.EntityFrameworkCore;
using VetCheckup.Application.Services.Persistence;

namespace VetCheckup.Infrastructure.Data;

public class ApplicationApplicationDbContext : DbContext, IApplicationDbContext
{
    #region Constructors

    public ApplicationApplicationDbContext() { }

    public ApplicationApplicationDbContext(DbContextOptions<ApplicationApplicationDbContext> options)
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

    void IApplicationDbContext.Add<TEntity>(TEntity entity)
    {
        if (base.Model.FindEntityType(nameof(TEntity)) == null)
            throw new NotSupportedException($"{nameof(TEntity)} is not currently tracked in the DbContext Model");

        base.Add(entity);
    }

    IQueryable<TEntity> IApplicationDbContext.Get<TEntity>() where TEntity : class
    {
        if (base.Model.FindEntityType(nameof(TEntity)) == null)
            throw new NotSupportedException($"{nameof(TEntity)} is not currently tracked in the DbContext Model");

        return base.Set<TEntity>();
    }

    void IApplicationDbContext.Remove<TEntity>(TEntity entity)
    {
        if (base.Model.FindEntityType(nameof(TEntity)) == null)
            throw new NotSupportedException($"{nameof(TEntity)} is not currently tracked in the DbContext Model");

        base.Remove(entity);
    }

    async Task IApplicationDbContext.SaveChangesAsync(CancellationToken cancellationToken)
    {
        try
        {
            using var _Transaction = await this.Database.BeginTransactionAsync(cancellationToken);
            {
                DeleteVetAddressAndContact();

                await base.SaveChangesAsync(cancellationToken);
            }
        }
        catch
        {
            await this.Database.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
    
    private void DeleteVetAddressAndContact()
    {
        var deletedEntities = this.ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted);
        foreach (var deletedVet in deletedEntities.Where(e => e.Entity is Vet))
        {
            var vet = deletedVet.Entity as Vet;
            if (vet == null)
                continue;

            this.Remove(vet.ContactDetails);
            this.Remove(vet.Address);
        }
    }

    #endregion

}
