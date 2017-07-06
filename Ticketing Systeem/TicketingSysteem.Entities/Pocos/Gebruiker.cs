using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TicketingSysteem.Entities.Enums;

namespace TicketingSysteem.Entities.Pocos
{
    public class Gebruiker : EntityBase
    {
        [Required]
        public string Voornaam { get; set; }
        [Required]
        public string Achternaam { get; set; }
        [Required]
        public string Email { get; set; }
        public string TelefoonNummer { get; set; }
        public string GsmNummer { get; set; }
        public int? VerantwoordelijkeId { get; set; }
        [ForeignKey("VerantwoordelijkeId")]
        public virtual Gebruiker Verantwoordelijke { get; set; }
        [Required]
        public Rol Rol { get; set; }
        public virtual ICollection<Issue>  Issues{ get; set; }

    }
}
