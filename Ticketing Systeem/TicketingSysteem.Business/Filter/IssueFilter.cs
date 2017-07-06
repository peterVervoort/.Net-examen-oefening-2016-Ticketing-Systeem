using System;
using System.Linq;
using TicketingSysteem.Entities.Pocos;
using TicketingSysteem.Entities.SearchPocos;

namespace TicketingSysteem.DataAccess
{
    public class IssueFilter : EntityFilterBase<Issue>, IEntityFilter<Issue>
    {
        public override IQueryable<Issue> Filter(IQueryable<Issue> query, SearchBase<Issue> searchCriteria)
        {
            if (searchCriteria.GetType().Equals(typeof(IssueSearch)))
            {
                IssueSearch criteria = (IssueSearch)searchCriteria;

                if (!string.IsNullOrWhiteSpace(criteria.GebruikerEmail))
                {
                    query = query.Where(x => x.Gebruiker.Email == criteria.GebruikerEmail);
                }

                if (!string.IsNullOrWhiteSpace(criteria.VerantwoordelijkeEmail))
                {
                    query = query.Where(x => x.Gebruiker.Verantwoordelijke.Email == criteria.VerantwoordelijkeEmail);
                }
                if (criteria.GebruikerId.HasValue)
                {
                    query = query.Where(x => x.GebruikerId == criteria.GebruikerId.Value);
                }

            }

            return base.Filter(query, searchCriteria);
        }
    }
}
