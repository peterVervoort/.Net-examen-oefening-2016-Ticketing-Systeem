using System;
using System.ComponentModel.DataAnnotations;

namespace TicketingSysteem.Entities.Pocos
{
    public class EntityBase
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }

    }
}
