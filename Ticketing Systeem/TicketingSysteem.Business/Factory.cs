using Microsoft.Practices.Unity;
using TicketingSysteem.Business.Repositories;
using TicketingSysteem.Business.Validators;
using TicketingSysteem.Business.Writers;
using TicketingSysteem.DataAccess;
using TicketingSysteem.Entities.Pocos;

namespace TicketingSysteem.Business
{
    public class Factory
    {
        public static void Configure(IUnityContainer container)
        {
            DataAccess.Factory.Configure(container);
            Entities.Factory.Configure(container);


            //Generic base classes
            container.RegisterType(typeof(IReader<>), typeof(ReaderBase<>));
            container.RegisterType(typeof(IWriter<>), typeof(WriterBase<>));
            container.RegisterType(typeof(IValidatorBase<>), typeof(ValidatorBase<>));

            //Custom writer
            container.RegisterType<IWriter<Issue>, IssueWriter>();

            //Custom validators
            container.RegisterType<IValidatorBase<Gebruiker>, GebruikerValidator>();

            //Custom Readers
            container.RegisterType<IReader<Gebruiker>, GebruikerReader>();

            //Custom filters
            container.RegisterType<IEntityFilter<Gebruiker>, GebruikerFilter>();
            container.RegisterType<IEntityFilter<Issue>, IssueFilter>();
            container.RegisterType<IEntityFilter<IssueStatus>, IssueStatusFilter>();
            container.RegisterType<IEntityFilter<ExtraInfo>, ExtraInfoFilter>();
        }
    }
}
