using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SimpleMemberShip.Models;

namespace SimpleMemberShip.Controllers
{
    public class EvennementController : Controller
    {
        private Model1Container db = new Model1Container();

        // GET: /Evennement/
        public ActionResult Index()
        {
            var evennementset = db.EvennementSet.Include(e => e.UsersSet);
            return View(evennementset.ToList());
        }

        // GET: /Evennement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvennementSet evennementset = db.EvennementSet.Find(id);
            if (evennementset == null)
            {
                return HttpNotFound();
            }
            return View(evennementset);
        }

        // GET: /Evennement/Create
        public ActionResult Create()
        {
            ViewBag.UsersId = new SelectList(db.UsersSet, "Id", "Username");
            return View();
        }

        // POST: /Evennement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Titre,Place,Date,UsersId")] EvennementSet evennementset)
        {
            if (ModelState.IsValid)
            {
                db.EvennementSet.Add(evennementset);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UsersId = new SelectList(db.UsersSet, "Id", "Username", evennementset.UsersId);
            return View(evennementset);
        }

        // GET: /Evennement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvennementSet evennementset = db.EvennementSet.Find(id);
            if (evennementset == null)
            {
                return HttpNotFound();
            }
            ViewBag.UsersId = new SelectList(db.UsersSet, "Id", "Username", evennementset.UsersId);
            return View(evennementset);
        }

        // POST: /Evennement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Titre,Place,Date,UsersId")] EvennementSet evennementset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evennementset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UsersId = new SelectList(db.UsersSet, "Id", "Username", evennementset.UsersId);
            return View(evennementset);
        }

        // GET: /Evennement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvennementSet evennementset = db.EvennementSet.Find(id);
            if (evennementset == null)
            {
                return HttpNotFound();
            }
            return View(evennementset);
        }

        // POST: /Evennement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EvennementSet evennementset = db.EvennementSet.Find(id);
            db.EvennementSet.Remove(evennementset);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult Recherche(string mot)
        {
            if (mot == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var q = from t in db.EvennementSet
                    where t.Titre.Contains(mot)
                    select t;
            if (q == null)
            {
                return HttpNotFound();
            }
            return View(q);
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
