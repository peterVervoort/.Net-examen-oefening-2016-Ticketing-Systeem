using System.Threading.Tasks;
using TicketingSysteem.Business.Helpers;
using TicketingSysteem.Entities.Pocos;

namespace TicketingSysteem.Business.Writers
{
    public interface IWriter<TEntity> where TEntity : EntityBase
    {
        Task<EntityResult<TEntity>> DeleteAsync(int id);
        Task<EntityResult<TEntity>> InsertAsync(TEntity entity);
        Task<EntityResult<TEntity>> UpdateAsync(TEntity entity);
    }
}