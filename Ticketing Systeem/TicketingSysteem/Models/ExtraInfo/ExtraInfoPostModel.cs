using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketingSysteem.Models.ExtraInfo
{
    public class ExtraInfoPostModel : PostModelBase<Entities.Pocos.ExtraInfo>
    {
        [Required]
        public int IssueStatusId { get; set; }
        [Required]
        public string InfoVraag { get; set; }
        public string InfoAntwoord { get; set; }
    }
}