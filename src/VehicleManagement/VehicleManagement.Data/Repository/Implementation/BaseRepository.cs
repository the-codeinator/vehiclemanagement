using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleManagement.Data.Repository.Interface;
using VehicleManagement.Domain.Entities;

namespace VehicleManagement.Data.Repository.Implementation
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : Base
    {
        protected BaseRepository()
        {

        }
        private readonly VehicleDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSets;
        public BaseRepository(VehicleDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException();
            _dbSets = _dbContext.Set<TEntity>();
        }
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSets.AddAsync(entity);
            return entity;
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
            GC.SuppressFinalize(this);
        }

        public virtual async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSets.Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _dbSets.FirstOrDefaultAsync(f => f.Id == id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSets.ToListAsync();
        }

        public virtual TEntity Delete(TEntity entity)
        {
             _dbSets.Remove(entity);
            return entity;
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
