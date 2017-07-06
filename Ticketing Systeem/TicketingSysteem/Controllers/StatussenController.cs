using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using TicketingSysteem.Business.Repositories;
using TicketingSysteem.Entities.Enums;
using TicketingSysteem.Entities.Pocos;

namespace TicketingSysteem.Controllers
{
    public class StatussenController : ApiController
    {
        private readonly IReader<Gebruiker> _gebruikerReader;
        private readonly IReader<Issue> _issueReader;

        public StatussenController(IReader<Issue> issueReader, IReader<Gebruiker> gebruikerReader)
        {
            _issueReader = issueReader;
            _gebruikerReader = gebruikerReader;
        }
        
        // GET: api/Statussen
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(Enum.GetNames(typeof(IssueStatusBeschrijving)));
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("~/api/issues/{issueId:int}/statussen")]
        public async Task<IHttpActionResult> GetPossibleStatussen(int issueId)
        {
            try
            {
                var issue = await _issueReader.GetById(issueId, nameof(Issue.IssueStatussen));
                if (issue == null) return NotFound();
                IssueStatus status = issue.IssueStatussen.OrderByDescending(i => i.CreationDate).FirstOrDefault();
                if (status == null) return Ok(IssueStatusBeschrijving.Nieuw);

                //Huidige rol bekijken
                var rol = (await _gebruikerReader.Filter(g => g.Email == User.Identity.Name)).FirstOrDefault()?.Rol;
                if (rol == null) return NotFound();


                List<IssueStatusBeschrijving> possibleStatussen = new List<IssueStatusBeschrijving>();
                switch (status.StatusBeschrijving)
                {
                    case IssueStatusBeschrijving.Nieuw:
                        if (CheckRole(rol, Rol.Dispatcher)) possibleStatussen.Add(IssueStatusBeschrijving.Toegewezen);
                        if (CheckRole(rol, Rol.Gebruiker, Rol.Manager, Rol.Dispatcher)) possibleStatussen.Add(IssueStatusBeschrijving.Canceled);
                        break;
                    case IssueStatusBeschrijving.Toegewezen:
                        if (CheckRole(rol, Rol.Solver, Rol.Dispatcher)) possibleStatussen.Add(IssueStatusBeschrijving.InBehandeling);
                        if (CheckRole(rol, Rol.Gebruiker, Rol.Manager)) possibleStatussen.Add(IssueStatusBeschrijving.Canceled);
                        break;
                    case IssueStatusBeschrijving.InBehandeling:
                        if (CheckRole(rol, Rol.Solver, Rol.Dispatcher)) possibleStatussen.Add(IssueStatusBeschrijving.Opgelost);
                        if (CheckRole(rol, Rol.Solver, Rol.Dispatcher)) possibleStatussen.Add(IssueStatusBeschrijving.ExtraInfo);
                        if (CheckRole(rol, Rol.Solver, Rol.Dispatcher)) possibleStatussen.Add(IssueStatusBeschrijving.Geweigerd);
                        if (CheckRole(rol, Rol.Gebruiker, Rol.Manager)) possibleStatussen.Add(IssueStatusBeschrijving.Canceled);
                        break;
                    case IssueStatusBeschrijving.Opgelost:
                        if (CheckRole(rol, Rol.Gebruiker, Rol.Manager)) possibleStatussen.Add(IssueStatusBeschrijving.Afgesloten);
                        if (CheckRole(rol, Rol.Gebruiker, Rol.Manager)) possibleStatussen.Add(IssueStatusBeschrijving.Canceled);
                        break;
                    case IssueStatusBeschrijving.ExtraInfo:
                        if (CheckRole(rol, Rol.Gebruiker, Rol.Manager)) possibleStatussen.Add(IssueStatusBeschrijving.InBehandeling);
                        if (CheckRole(rol, Rol.Gebruiker, Rol.Manager)) possibleStatussen.Add(IssueStatusBeschrijving.Canceled);
                        break;
                    case IssueStatusBeschrijving.Geweigerd:
                        if (CheckRole(rol, Rol.Dispatcher)) possibleStatussen.Add(IssueStatusBeschrijving.Toegewezen);
                        if (CheckRole(rol, Rol.Gebruiker, Rol.Manager, Rol.Dispatcher)) possibleStatussen.Add(IssueStatusBeschrijving.Canceled);
                        break;
                    case IssueStatusBeschrijving.Afgesloten:
                    case IssueStatusBeschrijving.Canceled:
                        break;
                    default:
                        break;
                }

                return Ok(possibleStatussen.Select(ps => ps.ToString()));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        private static bool CheckRole(Rol? currentRole, params Rol[] validRoles)
        {
            if (currentRole == Rol.Administrator) return true;
            if (!currentRole.HasValue) return false;
            return validRoles.Any(r => r == currentRole.Value);
        }
    }
}