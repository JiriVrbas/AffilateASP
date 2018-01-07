using DbLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AffilateWeb.Controllers
{
    public class UserManagementController : Controller
    {
        // GET: UserManagement
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Withdraw(string cisloUctu, string kodBanky, string castka)
        {
            if (ModelState.IsValid)
            {
                decimal castkaInt = 0;
                if (!string.IsNullOrEmpty(cisloUctu) && !string.IsNullOrEmpty(kodBanky) && decimal.TryParse(castka, out castkaInt))
                {
                    var user = Session["user"] as User;
                    if (user != null)
                    {
                        var withdraw = new Cashout()
                        {
                            AccountNumber = cisloUctu,
                            Amount = castkaInt,
                            Apply = DateTime.Now,
                            BankCode = kodBanky,
                            IsPaid = false,
                            UserId = user.UserId
                        };
                        using (var db = new Database())
                        {
                            db.Cashouts.Add(withdraw);
                            var usr = db.Users.FirstOrDefault(x => x.UserId == user.UserId);
                            Session["user"] = usr;
                            usr.Balance -= castkaInt;
                            db.SaveChanges();
                        }
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}