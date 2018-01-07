using AffilateWeb.Servicies;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AffilateWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            AffilatePartnersService apService = new AffilatePartnersService();

            return View(apService.UserPartners);
        }

        [HttpGet]
        public ActionResult Index(string link)
        {
            Session["link"] = link;
            AffilatePartnersService apService = new AffilatePartnersService(link);

            return View(apService.UserPartners);
        }
    }
}