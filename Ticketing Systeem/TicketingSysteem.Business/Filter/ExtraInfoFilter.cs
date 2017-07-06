using System;
using System.Linq;
using TicketingSysteem.Entities.Pocos;
using TicketingSysteem.Entities.SearchPocos;

namespace TicketingSysteem.DataAccess
{
    public class ExtraInfoFilter : EntityFilterBase<ExtraInfo>, IEntityFilter<ExtraInfo>
    {
        public override IQueryable<ExtraInfo> Filter(IQueryable<ExtraInfo> query, SearchBase<ExtraInfo> searchCriteria)
        {
            if (searchCriteria.GetType().Equals(typeof(ExtraInfoSearch)))
            {
                ExtraInfoSearch criteria = (ExtraInfoSearch)searchCriteria;

                if (criteria.IssueStatusId.HasValue)
                {
                    query = query.Where(x => x.IssueStatusId == criteria.IssueStatusId);
                }

            }

            return base.Filter(query, searchCriteria);
        }
    }
}
