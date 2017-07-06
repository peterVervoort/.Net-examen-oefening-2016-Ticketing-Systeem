using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSysteem.Entities.Enums;

namespace TicketingSysteem.Entities.Pocos
{
    public class Issue : EntityBase
    {
        [Required]
        public string Titel { get; set; }
        [Required]
        public string Beschrijving { get; set; }
        public IssueNiveau Niveau { get; set; }
        public DateTime IssueDate { get; set; }
        [Required]
        public int GebruikerId { get; set; }
        [ForeignKey("GebruikerId")]
        public virtual Gebruiker Gebruiker { get; set; }
        public virtual ICollection<IssueStatus> IssueStatussen { get; set; }
        public string Oplossing { get; set; }

    }
}
