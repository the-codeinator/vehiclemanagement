using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VehicleManagement.Domain.Entities;

namespace VehicleManagement.Data.Repository.Interface
{
    public interface IRepository<TEntity>: IDisposable where TEntity : IEntity
    {
        Task<TEntity> GetByIdAsync(Guid id);
        Task<TEntity> AddAsync(TEntity entity);
        TEntity Remove(TEntity entity);
        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity,
            bool>> predicate);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<int> SaveChangesAsync();

    }
}
