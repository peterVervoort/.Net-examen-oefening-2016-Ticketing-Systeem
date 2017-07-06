using TicketingSysteem.Business.Repositories;
using TicketingSysteem.Entities.Pocos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketingSysteem.Business.Validators
{
    class GebruikerValidator : ValidatorBase<Gebruiker> , IValidatorBase<Gebruiker>
    {
        private readonly IReader<Gebruiker> _reader;

        public GebruikerValidator(IReader<Gebruiker> reader)
        {
            _reader = reader;
        }

        public override async Task<IEnumerable<string>> ValidateAsync(Gebruiker entity)
        {
            List<string> messages = new List<string>();

            //int emailCount = await _reader.SearchCount(new UserSearchCriteria { Email = entity.Email });
            //if (emailCount > 0)
            //{
            //    messages.Add($"{nameof(User.Email)}: {entity.Email} has already been used");
            //}

            //int userNameCount = await _reader.SearchCount(new UserSearchCriteria { UserName = entity.UserName});
            //if (userNameCount > 0)
            //{
            //    messages.Add($"{nameof(User.UserName)}: {entity.UserName} has already been used");
            //}

            //int sameNameCount = await _reader.SearchCount(new UserSearchCriteria { FirstName = entity.FirstName, Name = entity.Name });
            //if (sameNameCount > 0)
            //{
            //    messages.Add($"This {nameof(User)} has already been added");
            //}

            return await Task.FromResult(messages);
        }

        public override async Task<IEnumerable<string>> ValidateUpdateAsync(Gebruiker entity)
        {
            List<string> messages = new List<string>();

            //int emailCount = await _reader.SearchCount(new UserSearchCriteria { Email = entity.Email });
            //if (emailCount > 1)
            //{
            //    messages.Add($"{nameof(User.Email)}: {entity.Email} has already been used");
            //}
            //if (emailCount == 0)
            //{
            //    messages.Add($"{nameof(User.Email)} cannot be changed");
            //}

            return await Task.FromResult(messages);
        }

    }
}
