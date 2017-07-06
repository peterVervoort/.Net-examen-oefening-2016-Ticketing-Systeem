using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketingSysteem.Models.ExtraInfo
{
    public class ExtraInfoModel : ModelBase<Entities.Pocos.ExtraInfo>
    {
        public int IssueStatusId { get; set; }
        public string InfoVraag { get; set; }
        public string InfoAntwoord { get; set; }
    }
}