using TicketingSysteem.Entities.Pocos;

namespace TicketingSysteem.Entities.SearchPocos
{
    public class SearchBase<TEntity> where TEntity : EntityBase
    {
        public int? Id { get; set; }
    }
}
