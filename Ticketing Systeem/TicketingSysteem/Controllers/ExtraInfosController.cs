using System.Web.Http;
using TicketingSysteem.Entities.Pocos;
using TicketingSysteem.Entities.SearchPocos;
using TicketingSysteem.Models.ExtraInfo;

namespace TicketingSysteem.Controllers
{
    [Authorize]
    public class ExtraInfosController : BaseController<ExtraInfo, ExtraInfoModel, ExtraInfoPostModel, ExtraInfoSearch>
    {
        
    }
}
