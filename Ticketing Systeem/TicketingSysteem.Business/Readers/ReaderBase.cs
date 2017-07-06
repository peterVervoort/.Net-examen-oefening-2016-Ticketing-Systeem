using TicketingSysteem.DataAccess.Repositories;
using TicketingSysteem.Entities.Pocos;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.Practices.Unity;
using System.Linq.Expressions;
using TicketingSysteem.DataAccess;
using TicketingSysteem.Entities.SearchPocos;

namespace TicketingSysteem.Business.Repositories
{
    public class ReaderBase<TEntity> : IReader<TEntity> where TEntity : EntityBase
    {
        [Dependency]
        public IRepository<TEntity> Repository { get; set; }


        public async Task<TEntity> GetById(int id, params string[] includes)
        {
            return await Repository.GetAsync(id, includes);
        }

        public virtual async Task<IEnumerable<TEntity>> Filter(Expression<Func<TEntity, bool>> filter, params string[] includes)
        {
            return await Repository.Search(filter, includes);
        }
        public async Task<IEnumerable<TEntity>> Filter(SearchBase<TEntity> search, params string[] includes)
        {
            return await Repository.Search(search, includes);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(params string[] includes)
        {
            return await Repository.GetAllAsync(includes);
        }

        public IEnumerable<string> GetIncludeList()
        {
            return Repository.GetIncludeList();
        }
    }
}
 