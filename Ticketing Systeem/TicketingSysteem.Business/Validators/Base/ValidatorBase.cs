using TicketingSysteem.Entities.Pocos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketingSysteem.Business.Validators
{
    public class ValidatorBase<Tentity> : IValidatorBase<Tentity> where Tentity : EntityBase
    {
        public virtual async Task<IEnumerable<string>> ValidateAsync(Tentity entity)
        {
            List<string> messages = new List<string>();
            return await Task.FromResult(messages);
        }

        public virtual async Task<IEnumerable<string>> ValidateInsertAsync(Tentity entity)
        {
            return await ValidateAsync(entity);
        }

        public virtual async Task<IEnumerable<string>> ValidateDeleteAsync(Tentity entity)
        {
            List<string> messages = new List<string>();
            return await Task.FromResult(messages);
        }

        public virtual async Task<IEnumerable<string>> ValidateUpdateAsync(Tentity entity)
        {
            return await ValidateAsync(entity);
        }

        public virtual async Task<IEnumerable<string>> ValidateRestoreAsync(Tentity entity)
        {
            return await ValidateAsync(entity);
        }
    }
}
