using HallOfFame.Domain.Entities;
using HallOfFame.Domain.Interfaces.Repositories;
using HallOfFame.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HallOfFame.Infrastructure.UnitOfWork
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly Context _context;
        private readonly DbSet<TEntity> _dataBase;

        public GenericRepository(Context context)
        {
            _context = context;
            _dataBase = context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dataBase.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dataBase.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dataBase.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(long id)
        {
            return await _dataBase.FindAsync(id);
        }

        public async Task<List<TEntity>> GetByPage(int page, int pageSize)
        {
            return await _dataBase.AsNoTracking().
                            Skip((page - 1) * pageSize).
                            Take(pageSize).ToListAsync();
        }

        public async Task RemoveAsync(long id)
        {
            await _dataBase.Where(e => e.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }
    }
}
