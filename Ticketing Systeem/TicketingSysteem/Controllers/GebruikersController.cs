using AutoMapper;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using TicketingSysteem.Entities.Enums;
using TicketingSysteem.Entities.Pocos;
using TicketingSysteem.Entities.SearchPocos;
using TicketingSysteem.Models;

namespace TicketingSysteem.Controllers
{
    [Authorize]
    public class GebruikersController : BaseController<Gebruiker, GebruikerModel, GebruikerPostModel, GebruikerSearch>
    {
        [AllowAnonymous]
        [ActionName("logininfo")]
        public async Task<IHttpActionResult> GetInfo([FromBody] SearchBase<Gebruiker> model)
        {
            try
            {
                if (User?.Identity == null) return Unauthorized();
                if (!User.Identity.IsAuthenticated) return Unauthorized();
                var currentUser = await EntityReader.Filter(g => g.Email == User.Identity.Name, nameof(Gebruiker.Verantwoordelijke));
                return Ok(Mapper.Map<GebruikerModel>(currentUser.FirstOrDefault()));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Authorize(Roles = nameof(Rol.Administrator))]
        public override Task<IHttpActionResult> Post([FromBody] GebruikerPostModel model)
        {
            return base.Post(model);
        }

        [Authorize(Roles = nameof(Rol.Administrator))]
        public override Task<IHttpActionResult> Delete(int id)
        {
            return base.Delete(id);
        }

        [Authorize(Roles = nameof(Rol.Administrator))]
        public override Task<IHttpActionResult> Put(int id, [FromBody] GebruikerPostModel model)
        {
            return base.Put(id, model);
        }
    }
}
