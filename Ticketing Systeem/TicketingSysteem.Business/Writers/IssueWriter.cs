using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSysteem.Entities.Pocos;

namespace TicketingSysteem.Business.Writers
{
    public class IssueWriter : WriterBase<Issue>
    {
        private readonly IWriter<IssueStatus> _issueStatusWriter;

        public IssueWriter(IWriter<IssueStatus> issueStatusWriter)
        {
            _issueStatusWriter = issueStatusWriter;
        }

        public override async Task PostInsert(Issue entity)
        {
            IssueStatus status = new IssueStatus();
            status.IssueId = entity.Id;
            status.StatusBeschrijving = Entities.Enums.IssueStatusBeschrijving.Nieuw;

            await _issueStatusWriter.InsertAsync(status);
        }
    }
}
