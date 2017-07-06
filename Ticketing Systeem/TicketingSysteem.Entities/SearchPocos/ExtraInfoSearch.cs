using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketingSysteem.Entities.Pocos;

namespace TicketingSysteem.Entities.SearchPocos
{
    public class ExtraInfoSearch : SearchBase<ExtraInfo>
    {
        public int? IssueStatusId { get; set; }
    }
}