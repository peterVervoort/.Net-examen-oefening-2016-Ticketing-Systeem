using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TicketingSysteem.Models.ExtraInfo;

namespace TicketingSysteem.Models.Issue
{
    public class IssueStatusModel : ModelBase<Entities.Pocos.IssueStatus>
    {
        [Required]
        public int IssueId { get; set; }
        [Required]
        public string StatusBeschrijving { get; set; }
        public int SolverId { get; set; }
        public string Solver { get; set; }
        public string AnnulatieReden { get; set; }
        public IEnumerable<ExtraInfoModel> ExtraInfos { get; set; }
        public DateTime CreationDate { get; set; }
    }
}




