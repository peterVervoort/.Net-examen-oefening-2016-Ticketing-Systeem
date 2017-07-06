using System.Collections.Generic;
using TicketingSysteem.Entities.Pocos;
using System.Threading.Tasks;

namespace TicketingSysteem.Business.Validators
{
    public interface IValidatorBase<Tentity> where Tentity : EntityBase
    {
        Task<IEnumerable<string>> ValidateAsync(Tentity entity);
        Task<IEnumerable<string>> ValidateInsertAsync(Tentity entity);
        Task<IEnumerable<string>> ValidateDeleteAsync(Tentity entity);
        Task<IEnumerable<string>> ValidateUpdateAsync(Tentity entity);
        Task<IEnumerable<string>> ValidateRestoreAsync(Tentity entity);
    }
}