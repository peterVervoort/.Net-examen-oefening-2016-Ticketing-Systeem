using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using TicketingSysteem.Business.Repositories;
using TicketingSysteem.Entities.Enums;
using TicketingSysteem.Entities.Pocos;
using TicketingSysteem.Entities.SearchPocos;
using TicketingSysteem.Models;
using TicketingSysteem.Models.Issue;

namespace TicketingSysteem.Controllers
{
    [Authorize]
    public class IssueStatussenController : BaseController<IssueStatus, IssueStatusModel, IssueStatusPostModel, IssueStatusSearch>
    {
        private readonly IReader<Gebruiker> _gebruikerReader;
        private readonly IReader<Issue> _issueReader;

        public IssueStatussenController(IReader<Gebruiker> gebruikerReader, IReader<Issue> issueReader)
        {
            _gebruikerReader = gebruikerReader;
            _issueReader = issueReader;
        }

        public override async Task<IHttpActionResult> Post([FromBody] IssueStatusPostModel model)
        {
            if (model.StatusBeschrijving == IssueStatusBeschrijving.InBehandeling.ToString()
                || model.StatusBeschrijving == IssueStatusBeschrijving.ExtraInfo.ToString())
            {
                var isssue = await _issueReader.GetById(model.IssueId, nameof(Issue.IssueStatussen));
                if (isssue == null) return NotFound();
                var lastStatus = isssue.IssueStatussen.OrderBy(x => x.CreationDate).LastOrDefault();
                if (lastStatus.StatusBeschrijving == IssueStatusBeschrijving.ExtraInfo)
                {
                    model.SolverId = lastStatus.SolverId;
                } else
                {
                    var user = await _gebruikerReader.Filter(g => g.Email == User.Identity.Name);
                    if (user == null || user.FirstOrDefault() == null) return Unauthorized();
                    model.SolverId = user.FirstOrDefault().Id;
                }
            }
            return await base.Post(model);
        }
    }
}
