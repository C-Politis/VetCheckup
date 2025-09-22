namespace VetCheckup.Application.Services.Persistence;
public interface IApplicationDbContext
{
    #region Methods

    void Add<TEntity>(TEntity entity) where TEntity : class;

    IQueryable<TEntity> Get<TEntity>() where TEntity : class;

    void Remove<TEntity>(TEntity entity) where TEntity : class;

    Task SaveChangesAsync(CancellationToken cancellationToken);

    #endregion

}
