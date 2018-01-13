using SimpleMemberShip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using System.Web.Security;

namespace SimpleMemberShip.Controllers
{
    public class AccountController : Controller
    {
        private Model1Container db = new Model1Container();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UsersSet logindata, string ReturnUrl )
        {
            if (ModelState.IsValid)
            {
                if (WebSecurity.Login(logindata.Username, logindata.Password))
                {
                    Session["IdUser"] = logindata.Id;
                    Session["Username"] = logindata.Username;

                    if (ReturnUrl != null)
                    {
                        return Redirect(ReturnUrl);
                    }
                    return RedirectToAction("index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Nom d'utilisateur ou Mot de passe invalides !");
                    return View(logindata);
                }

            }
            ModelState.AddModelError("", "Nom d'utilisateur ou Mot de passe invalides !");
            return View(logindata);
        }
           

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Id,Username,Password,Nom,Prenom,Sexe")] UsersSet users, string role, string sexe)
        {
            if (ModelState.IsValid)
            {
                WebSecurity.CreateUserAndAccount(users.Username, users.Password);
                Roles.AddUserToRole(users.Username, role);
                db.UsersSet.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            return View(users);
        }

        public ActionResult Logout()
        {
            WebSecurity.Logout();
            Session.Clear();
            return RedirectToAction("login", "Users");
        }
	}
}