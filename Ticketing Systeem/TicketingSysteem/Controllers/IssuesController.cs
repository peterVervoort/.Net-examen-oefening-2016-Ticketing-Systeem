using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using TicketingSysteem.Entities.Enums;
using TicketingSysteem.Entities.Pocos;
using TicketingSysteem.Entities.SearchPocos;
using TicketingSysteem.Models.Issue;

namespace TicketingSysteem.Controllers
{
    [Authorize]
    public class IssuesController : BaseController<Issue, IssueModel, IssuePostModel, IssueSearch>
    {
        public override Task<IHttpActionResult> Get()
        {
            return Get("gebruiker,issuestatussen,issuestatussen.solver");
        }

        public override async Task<IHttpActionResult> Get([FromUri] string fields)
        {
            try
            {
                IEnumerable<Issue> entities = null;
                var userName = User.Identity.Name;
                
                if (User.IsInRole(Rol.Administrator))
                {
                    entities = await EntityReader.GetAllAsync(fields.Split(','));
                } else if (User.IsInRole(Rol.Manager))
                {
                    entities = await EntityReader.Filter(e => e.Gebruiker.Email == userName || e.Gebruiker.Verantwoordelijke.Email == userName, fields.Split(','));
                }
                else if (User.IsInRole(Rol.Dispatcher))
                {
                    entities = await EntityReader.Filter(e => e.Gebruiker.Email == userName
                        || (e.IssueStatussen.OrderByDescending(x => x.CreationDate).FirstOrDefault().Solver.Email == userName)
                        || e.IssueStatussen.OrderByDescending(x => x.CreationDate).FirstOrDefault().StatusBeschrijving == IssueStatusBeschrijving.Geweigerd
                        || e.IssueStatussen.OrderByDescending(x => x.CreationDate).FirstOrDefault().StatusBeschrijving == IssueStatusBeschrijving.Nieuw, fields.Split(','));
                }
                else if (User.IsInRole(Rol.Solver))
                {
                    entities = await EntityReader.Filter(e => e.Gebruiker.Email == userName
                        || (e.IssueStatussen.OrderByDescending(x => x.CreationDate).FirstOrDefault().Solver.Email == userName), fields.Split(','));
                }
                else if (User.IsInRole(Rol.Gebruiker))
                {
                    entities = await EntityReader.Filter(e => e.Gebruiker.Email == userName, fields.Split(','));
                }


                return Ok(Mapper.Map<IEnumerable<IssueModel>>(entities));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        public override async Task<IHttpActionResult> GetById(int id)
        {
            return await GetById(id, "gebruiker,issuestatussen,issuestatussen.solver");
        }

        public override async Task<IHttpActionResult> GetById(int id, [FromUri] string fields)
        {
            try
            {
                var entity = await EntityReader.GetById(id, nameof(Issue.Gebruiker), $"{nameof(Issue.Gebruiker)}.{nameof(Gebruiker.Verantwoordelijke)}", nameof(Issue.IssueStatussen), $"{nameof(Issue.IssueStatussen)}.{nameof(IssueStatus.Solver)}");
                if (entity == null) return NotFound();

                if (User.IsInRole(Rol.Manager))
                {
                    if (!(entity.Gebruiker.Email == User.Identity.Name || entity.Gebruiker.Verantwoordelijke.Email == User.Identity.Name)) return Unauthorized();
                } else if (User.IsInRole(Rol.Dispatcher))
                {
                    if (entity.Gebruiker.Email != User.Identity.Name)
                    {
                        var last = entity.IssueStatussen.OrderBy(x => x.CreationDate).LastOrDefault();
                        if (last == null) return Unauthorized();
                        switch (last.StatusBeschrijving)
                        {
                            case IssueStatusBeschrijving.Geweigerd:
                            case IssueStatusBeschrijving.Nieuw:
                                break;
                            default:
                                if (entity.IssueStatussen.OrderBy(x => x.CreationDate).LastOrDefault().Solver.Email !=
                            User.Identity.Name) return Unauthorized();
                                break;
                        }
                    }
                } else if (User.IsInRole(Rol.Solver))
                {
                    if (entity.Gebruiker.Email != User.Identity.Name)
                    {
                        if (entity.IssueStatussen.OrderBy(x => x.CreationDate).LastOrDefault().Solver.Email !=
                            User.Identity.Name) return Unauthorized();
                    }
                } else if (User.IsInRole(Rol.Gebruiker))
                {
                    if (entity.Gebruiker.Email != User.Identity.Name) return Unauthorized();
                }

                return Ok(Mapper.Map<IssueModel>(entity));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [Authorize(Roles = "Dispatcher, Administrator, Solver")]
        public override Task<IHttpActionResult> Put(int id, [FromBody] IssuePostModel model)
        {
            return base.Put(id, model);
        }

        public override async Task<IHttpActionResult> Delete(int id)
        {
            if (User.IsInRole(Rol.Manager, Rol.Administrator))
            {
                var issue = await EntityReader.GetById(id, nameof(Issue.Gebruiker), $"{nameof(Issue.Gebruiker)}.{nameof(Gebruiker.Verantwoordelijke)}");
                if (issue.Gebruiker.Email == User.Identity.Name || issue.Gebruiker.Verantwoordelijke.Email == User.Identity.Name) return Unauthorized();
            } else
            {
                var issue = await EntityReader.GetById(id, nameof(Issue.Gebruiker));
                if (issue.Gebruiker.Email != User.Identity.Name) return Unauthorized();
            }
            return await base.Delete(id);
        }
    }
}