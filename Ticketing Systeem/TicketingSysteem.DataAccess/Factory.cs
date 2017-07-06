using Microsoft.Practices.Unity;
using TicketingSysteem.DataAccess.Repositories;
using TicketingSysteem.Entities.Pocos;

namespace TicketingSysteem.DataAccess
{
    public class Factory
    {
        public static void Configure(IUnityContainer container)
        {
            container.RegisterType(typeof(IRepository<>), typeof(RepositoryBase<>));
            container.RegisterType(typeof(IEntityFilter<>), typeof(EntityFilterBase<>));

        }
    }
}
