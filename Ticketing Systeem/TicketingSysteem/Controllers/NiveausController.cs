using System;
using System.Web.Http;
using TicketingSysteem.Entities.Enums;

namespace TicketingSysteem.Controllers
{
    public class NiveausController : ApiController
    {
        // GET: Niveaus
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(Enum.GetNames(typeof(IssueNiveau)));
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }
    }
}