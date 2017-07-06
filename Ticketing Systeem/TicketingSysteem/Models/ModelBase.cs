using System;
using TicketingSysteem.Entities.Pocos;

namespace TicketingSysteem.Models
{
    public class ModelBase<TEntity> where TEntity : EntityBase
    {
        public int Id { get; set; }
        public DateTime? CreationDate { get; set; }
    }

    public class PostModelBase<TEntity> where TEntity : EntityBase
    {
        public int Id { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}