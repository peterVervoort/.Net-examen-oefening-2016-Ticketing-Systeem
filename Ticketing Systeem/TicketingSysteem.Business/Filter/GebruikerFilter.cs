using System;
using System.Linq;
using TicketingSysteem.Entities.Enums;
using TicketingSysteem.Entities.Pocos;
using TicketingSysteem.Entities.SearchPocos;

namespace TicketingSysteem.DataAccess
{
    public class GebruikerFilter : EntityFilterBase<Gebruiker>,  IEntityFilter<Gebruiker>
    {
        public override IQueryable<Gebruiker> Filter(IQueryable<Gebruiker> query, SearchBase<Gebruiker> searchCriteria)
        {
            if (searchCriteria.GetType().Equals(typeof(GebruikerSearch)))
            {

                GebruikerSearch criteria = (GebruikerSearch)searchCriteria;

                if (!string.IsNullOrWhiteSpace(criteria.Email))
                {
                    query = query.Where(x => x.Email == criteria.Email);
                }
                if (!string.IsNullOrEmpty(criteria.Rol))
                {
                    Rol rol;
                    if (Enum.TryParse<Rol>(criteria.Rol, out rol))
                    {
                        query = query.Where(x => x.Rol == rol);
                    }
                }
                if (criteria.MinionOfId.HasValue)
                {
                    query = query.Where(x => x.VerantwoordelijkeId == criteria.MinionOfId.Value);
                }
            }

            return base.Filter(query, searchCriteria);
        }
    }
}
