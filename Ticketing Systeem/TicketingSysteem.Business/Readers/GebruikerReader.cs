using TicketingSysteem.DataAccess.Repositories;
using TicketingSysteem.Entities.Pocos;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace TicketingSysteem.Business.Repositories
{
    public class GebruikerReader : ReaderBase<Gebruiker> 
    {
        //public override async Task<IEnumerable<Gebruiker>> Filter(string filter, params string[] includes)
        //{
        //    var pairs = filter.Split(';');
        //    foreach (var pair in pairs)
        //    {
        //        var keyvalue = pair.Split(':');
        //        if (keyvalue.Length != 2) continue;

        //        var field = keyvalue[0];
        //        var value = keyvalue[1];

        //        if (field.Equals(nameof(Gebruiker.Email), StringComparison.InvariantCultureIgnoreCase))
        //        {
        //            return await Repository.Search(x => x.Email == value, includes);
        //        }
        //    }
        //    return await Repository.GetAllAsync(includes);
        //}

    }
}
 