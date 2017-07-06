using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketingSysteem.Entities.Pocos;

namespace TicketingSysteem.Entities.SearchPocos
{
    public class IssueSearch : SearchBase<Issue>
    {
        public string VerantwoordelijkeEmail { get; set; }
        public string GebruikerEmail { get; set; }
        public int? GebruikerId { get; set; }
    }
}