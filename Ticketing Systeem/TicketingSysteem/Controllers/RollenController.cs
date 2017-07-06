using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using TicketingSysteem.Entities.Enums;
using TicketingSysteem.Entities.Pocos;
using TicketingSysteem.Models;

namespace TicketingSysteem.Controllers
{
    public class RollenController : ApiController
    {
        // GET: api/TEntity
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(Enum.GetNames(typeof(Rol)));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
