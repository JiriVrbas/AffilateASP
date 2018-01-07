using AffilateWeb.Servicies;
using AffilateWeb.Utils;
using DbLayer;
using Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace AffilateWeb.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Login(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                TempData["error-message"] = "Přihlašovací jméno nebo heslo nejsou vyplňěny";
                return RedirectToAction("Index", "Home");
            }

            var hashedPassword = PasswordHashing.HashString(password);

            User user;
            using (var db = new Database())
            {
                user = db.Users.FirstOrDefault(x => x.Email == login && x.Password == hashedPassword);
            }

            if (user != null)
            {
                Session["user"] = user;
            }
            else
            {
                TempData["error-message"] = "Chybné uživatelské jméno nebo heslo";
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult FacebookLogin(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                TempData["error-message"] = "Neplatné přihlášení pomocí facebooku";
                return RedirectToAction("Index", "Home");
            }

            User user;
            using (var db = new Database())
            {
                user = db.Users.FirstOrDefault(x => x.FacebookAccount.Email == email);
            }

            if (user != null)
            {
                Session["user"] = user;
            }
            else
            {
                TempData["error-message"] = "Nepodařilo se načíst informace o uživateli";
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult FacebookSignUp(string email, string fullname, string gender)
        {
            var user = new User()
            {
                Email = email,
                IsConfirmed = false
            };
            var facebook = new FacebookIdentity()
            {
                Email = email,
                Created = DateTime.Now,
                Gender = gender               
            };
            var name = fullname.Split(' ');
            if(!string.IsNullOrEmpty(name[0]))
            {
                facebook.FirstName = name[0];
            }
            if (!string.IsNullOrEmpty(name[1]))
            {
                facebook.LastName = name[1];
            }
            user.FacebookAccount = facebook;

            var confirmationService = new ConfirmationService();
            user.ConfirmationLink = confirmationService.GetUniqeConfirmationLink(HttpContext.Request.Url.Authority);
            var defaultPassword = Generators.GetRandomUniqueLink(10);
            user.Password = PasswordHashing.HashString(defaultPassword);
            using (var db = new Database())
            {
                var superiorLink = Session["link"];
                if (superiorLink != null)
                {
                    user.SupperiorId = db.Users.FirstOrDefault(x => x.Link == superiorLink).UserId;
                }
                if (db.Users.Any(x => x.Email == email))
                {
                    TempData["error-message"] = "Uživatel s tímto emailem již existuje";
                    return RedirectToAction("Index", "Home");
                }

                user.Link = Generators.GetRandomUniqueLink(7);

                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Users.Add(user);
                        db.SaveChanges();

                        confirmationService.SendConfirmationEmail(user.Email, user.ConfirmationLink);
                        //TODO: odeslat email s defaultním heslem?? Nabídnout změnu hesla
                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult SignUp(string email, string pwd)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    Email = email,
                    IsConfirmed = false
                };
                var confirmationService = new ConfirmationService();
                user.ConfirmationLink = confirmationService.GetUniqeConfirmationLink(HttpContext.Request.Url.Authority);
                user.Password = PasswordHashing.HashString(pwd);
                using (var db = new Database())
                {
                    var superiorLink = Session["link"];
                    if (superiorLink != null)
                    {
                        user.SupperiorId = db.Users.FirstOrDefault(x => x.Link == superiorLink).UserId;
                    }
                    if (db.Users.Any(x => x.Email == email))
                    {
                        TempData["error-message"] = "Uživatel s tímto emailem již existuje";
                        return RedirectToAction("Index", "Home");
                    }

                    user.Link = Generators.GetRandomUniqueLink(7);

                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            db.Users.Add(user);
                            db.SaveChanges();

                            confirmationService.SendConfirmationEmail(user.Email, user.ConfirmationLink);
                            dbContextTransaction.Commit();
                        }
                        catch (Exception)
                        {
                            dbContextTransaction.Rollback();
                        }
                    }
                }

            }

            return RedirectToAction("Index", "Home");
        }
    }
}