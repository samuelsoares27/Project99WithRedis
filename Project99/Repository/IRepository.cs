namespace Project99.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);
        IQueryable<TEntity> GetQueryable();
        Task<TEntity?> Insert(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<TEntity> Delete(TEntity entity);
        Task SaveChangesAsync();
    }
}
