using Microsoft.EntityFrameworkCore;
using Project99.Data;

namespace Project99.Repository
{
    public class Repository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : class
    {
        protected readonly DataContext _dataContext;
        public Repository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<TEntity> Delete(TEntity entity)
        {
            _dataContext.Set<TEntity>().Remove(entity);
            await _dataContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dataContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _dataContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity?> Insert(TEntity entity)
        {
            await _dataContext.Set<TEntity>().AddAsync(entity);
            await _dataContext.SaveChangesAsync();
            return entity;
        }

        public async Task SaveChangesAsync()
        {
            await _dataContext.SaveChangesAsync();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            _dataContext.Set<TEntity>().Update(entity);
            await _dataContext.SaveChangesAsync();
            return entity;
        }

        public IQueryable<TEntity> GetQueryable()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }
    }
}
