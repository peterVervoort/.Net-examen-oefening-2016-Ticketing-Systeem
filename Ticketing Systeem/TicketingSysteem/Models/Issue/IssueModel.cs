using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketingSysteem.Entities.Enums;

namespace TicketingSysteem.Models.Issue
{
    public class IssueModel : ModelBase<Entities.Pocos.Issue>
    {
        public string  Titel { get; set; }
        public string Beschrijving { get; set; }
        public string Niveau { get; set; }
        public DateTime IssueDate { get; set; }
        public int GebruikerId { get; set; }
        public string Oplossing { get; set; }
        public string Gebruiker { get; set; }
        public string HuidigeStatus { get; set; }
        public string Solver { get; set; }
    }
}




