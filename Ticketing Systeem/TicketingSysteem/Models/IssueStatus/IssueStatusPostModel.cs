using System.ComponentModel.DataAnnotations;

namespace TicketingSysteem.Models.Issue
{
    public class IssueStatusPostModel : PostModelBase<Entities.Pocos.IssueStatus>
    {
        [Required]
        public int IssueId { get; set; }
        [Required]
        public string StatusBeschrijving { get; set; }
        public int? SolverId { get; set; }
        public string AnnulatieReden { get; set; }
    }
}