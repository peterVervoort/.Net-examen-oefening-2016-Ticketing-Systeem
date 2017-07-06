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
    public class IssueStatus : EntityBase
    {
        [Required]
        public int IssueId { get; set; }
        [ForeignKey("IssueId")]
        public virtual Issue Issue { get; set; }   
        [Required]
        public IssueStatusBeschrijving  StatusBeschrijving { get; set; }
        public int? SolverId { get; set; }
        [ForeignKey("SolverId")]
        public Gebruiker Solver { get; set; }
        public string AnnulatieReden { get; set; }
        public virtual ICollection<ExtraInfo> ExtraInfos { get; set; }
    }
}
