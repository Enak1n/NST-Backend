using System.Linq.Expressions;

namespace HallOfFame.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(long id);
        Task AddAsync(TEntity entity);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task RemoveAsync(long id);
        Task UpdateAsync(long id);
        Task<List<TEntity>> GetByPage(int page, int pageSize);
    }
}
