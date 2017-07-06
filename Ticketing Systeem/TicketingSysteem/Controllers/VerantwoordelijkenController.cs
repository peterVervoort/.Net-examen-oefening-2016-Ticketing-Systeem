using AutoMapper;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using TicketingSysteem.Business.Repositories;
using TicketingSysteem.Business.Writers;
using TicketingSysteem.Entities.Pocos;
using TicketingSysteem.Models;

namespace TicketingSysteem.Controllers
{
    public class VerantwoordelijkenController : ApiController
    {
        [Dependency]
        protected IReader<Gebruiker> EntityReader { get; set; }
        [Dependency]
        protected IWriter<Gebruiker> EntityWriter { get; set; }

        // GET: api/TEntity
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var entities = await EntityReader.GetAllAsync();
                var verantwoordelijken = entities.Where(g => g.Rol == Entities.Enums.Rol.Manager);
                return Ok(Mapper.Map<IEnumerable<GebruikerModel>>(verantwoordelijken));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
