using System;
using System.Linq;
using TicketingSysteem.Entities.Pocos;
using TicketingSysteem.Entities.SearchPocos;

namespace TicketingSysteem.DataAccess
{
    public class IssueStatusFilter : EntityFilterBase<IssueStatus>, IEntityFilter<IssueStatus>
    {
        public override IQueryable<IssueStatus> Filter(IQueryable<IssueStatus> query, SearchBase<IssueStatus> searchCriteria)
        {
            if (searchCriteria.GetType().Equals(typeof(IssueStatusSearch)))
            {
                IssueStatusSearch criteria = (IssueStatusSearch)searchCriteria;

                if (criteria.IssueId.HasValue)
                {
                    query = query.Where(x => x.IssueId == criteria.IssueId);
                }

            }

            return base.Filter(query, searchCriteria);
        }
    }
}
