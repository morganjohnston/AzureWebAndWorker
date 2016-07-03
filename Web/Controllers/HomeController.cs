using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public ActionResult ScheduleWidget()
        {
            busSender.SendMessage(new WidgetAdded("Schedule YO" + DateTimeOffset.UtcNow.AddMinutes(1).ToLocalTime().ToFileTime()), DateTimeOffset.UtcNow.AddMinutes(1));
            return View("Index");
        }
    }
}