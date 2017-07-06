using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSysteem.Entities.Pocos
{
    public class ExtraInfo : EntityBase
    {
        [Required]
        public int IssueStatusId { get; set; }
        [ForeignKey ("IssueStatusId")]
        public IssueStatus IssueStatus { get; set; }
        [Required]
        public string InfoVraag { get; set; }
        public string InfoAntwoord { get; set; }
    }
}
