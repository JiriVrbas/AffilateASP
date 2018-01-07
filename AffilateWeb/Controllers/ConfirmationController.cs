using AffilateWeb.Servicies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AffilateWeb.Controllers
{
    public class ConfirmationController : Controller
    {
        // GET: Confirmation
        public ActionResult Index(string clink)
        {
            if(clink != null)
            {
                var conSer = new ConfirmationService();
                conSer.Confirm(clink);
                TempData["error-message"] = "Váš email byl ověřen";
            }
            return View("Index","Home");//TODO: přesměrovat na úvodní stránku a dát to uživateli nějak vědět že je ověřen??
        }
    }
}