using System.Web.Mvc;
using Shared.Contracts;
using Web.Infrastructure;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBusSender busSender;

        public HomeController(IBusSender busSender)
        {
            this.busSender = busSender;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MakeWidget()
        {
            busSender.SendMessage(new WidgetAdded("YO"));
            return View("Index");
        }
    }
}