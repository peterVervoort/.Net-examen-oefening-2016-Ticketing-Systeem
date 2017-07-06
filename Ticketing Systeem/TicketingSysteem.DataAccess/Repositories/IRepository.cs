using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TicketingSysteem.Entities.Pocos;
using TicketingSysteem.Entities.SearchPocos;

namespace TicketingSysteem.DataAccess.Repositories
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        Task<TEntity> GetAsync(int id, IEnumerable<string> includes = null);
        Task<IList<TEntity>> GetAllAsync(IEnumerable<string> includes = null);

        Task<TEntity> GetDeletedAsync(int id, IEnumerable<string> includes = null);
        Task<IList<TEntity>> GetAllDeletedAsync(IEnumerable<string> includes = null);

        IEnumerable<string> GetIncludeList();

        Task<IList<TEntity>> Search(Expression<Func<TEntity, bool>> searchExpression, IEnumerable<string> includes = null);
        Task<IList<TEntity>> Search(SearchBase<TEntity> searchCriteria, IEnumerable<string> includes = null);

        Task<TEntity> CreateAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task DeleteAsync(int id);

    }
}