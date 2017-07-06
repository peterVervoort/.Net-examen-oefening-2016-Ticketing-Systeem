using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSysteem.Entities.Pocos;
using TicketingSysteem.Entities.SearchPocos;

namespace TicketingSysteem.DataAccess
{
    public class EntityFilterBase<TEntity> : IEntityFilter<TEntity> where TEntity : EntityBase
    {
        public virtual IQueryable<TEntity> Filter(IQueryable<TEntity> query, SearchBase<TEntity> searchCriteria)
        {
            if (query == null) return null;

            if (searchCriteria.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchCriteria.Id.Value);
            }

            return query;
        }
    }
}
