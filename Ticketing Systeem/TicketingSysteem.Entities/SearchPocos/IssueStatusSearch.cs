using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketingSysteem.Entities.Pocos;

namespace TicketingSysteem.Entities.SearchPocos
{
    public class IssueStatusSearch : SearchBase<IssueStatus>
    {
        public int? IssueId { get; set; }
    }
}