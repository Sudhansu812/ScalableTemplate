using Microsoft.EntityFrameworkCore;
using ScalableApplication.Application.Interfaces.Repositories;
using ScalableApplication.Infrastructure.Persistence;

namespace ScalableApplication.Infrastructure.Repositories
{
    public class BaseRepository<T>(AppDbContext db) : IRepository<T> where T : class
    {
        protected readonly AppDbContext _db = db;
        private readonly DbSet<T> _dbSet = db.Set<T>();

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(params object[] keyValues)
        {
            T? entity = await _dbSet.FindAsync(keyValues);
            return entity;
        }

        public Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
