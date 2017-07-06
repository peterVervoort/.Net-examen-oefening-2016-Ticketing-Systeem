using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingSysteem.Models;

namespace TicketingSysteem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
