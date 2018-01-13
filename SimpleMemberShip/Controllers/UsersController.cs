using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SimpleMemberShip.Models;
using System.Web.Security;

namespace SimpleMemberShip.Controllers
{
    public class UsersController : Controller
    {
        private Model1Container db = new Model1Container();

        // GET: /Users/
        public ActionResult Index()
        {
            return View(db.UsersSet.ToList());
        }

        // GET: /Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersSet usersset = db.UsersSet.Find(id);
            if (usersset == null)
            {
                return HttpNotFound();
            }
            return View(usersset);
        }

        // GET: /Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Username,Password,Nom,Prenom,Adresse,Role,Tel,Sexe")] UsersSet usersset)
        {
            if (ModelState.IsValid)
            {
                db.UsersSet.Add(usersset);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(usersset);
        }


        public ActionResult login()
        {


            return View();


        }


        [HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult login(UsersSet model)
        {

            Boolean userValid = db.UsersSet.Any(user => user.Username == model.Username && user.Password == model.Password);


            if (userValid)
            {
                UsersSet users = db.UsersSet.Single(user => user.Username == model.Username);

                FormsAuthentication.SetAuthCookie(users.Role, false);
                Session["Username"] = users.Username;
                Session["Id"] = users.Id;
                System.Diagnostics.Debug.WriteLine("id user = " + users.Id);

                return RedirectToAction("Index", "Home");


                //if (users.Role.Equals("admin"))
                //    return RedirectToAction("Index", "Users");

                //if (users.Role.Equals("etudiant"))
                //    return RedirectToAction("Index", "Home");


            }
            else
            {
                ViewBag.Erreur = "Login ou Le mot de passe que vous avez saisi est incorrect. Veuillez réessayer !!";
                return View();
            }


        }




        // GET: /Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersSet usersset = db.UsersSet.Find(id);
            if (usersset == null)
            {
                return HttpNotFound();
            }
            return View(usersset);
        }

        // POST: /Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Username,Password,Nom,Prenom,Adresse,Role,Tel,Sexe")] UsersSet usersset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usersset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usersset);
        }

        // GET: /Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersSet usersset = db.UsersSet.Find(id);
            if (usersset == null)
            {
                return HttpNotFound();
            }
            return View(usersset);
        }

        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UsersSet usersset = db.UsersSet.Find(id);
            db.UsersSet.Remove(usersset);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
