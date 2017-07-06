using System.Linq;
using TicketingSysteem.Entities.Pocos;
using TicketingSysteem.Entities.SearchPocos;

namespace TicketingSysteem.DataAccess
{
    public interface IEntityFilter<TEntity> where TEntity : EntityBase
    {
        IQueryable<TEntity> Filter(IQueryable<TEntity> query, SearchBase<TEntity> searchCriteria);
    }
}
