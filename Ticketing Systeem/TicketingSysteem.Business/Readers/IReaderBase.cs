using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TicketingSysteem.Entities.Pocos;
using TicketingSysteem.Entities.SearchPocos;

namespace TicketingSysteem.Business.Repositories
{
    public interface IReader<TEntity> where TEntity : EntityBase
    {
        IEnumerable<string> GetIncludeList();
        Task<IEnumerable<TEntity>> GetAllAsync(params string[] includes);
        Task<TEntity> GetById(int id, params string[] includes);
        Task<IEnumerable<TEntity>> Filter(Expression<Func<TEntity, bool>> filter, params string[] includes);
        Task<IEnumerable<TEntity>> Filter(SearchBase<TEntity> search, params string[] includes);
    }
}
 