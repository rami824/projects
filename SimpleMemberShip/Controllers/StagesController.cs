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
    public class StagesController : Controller
    {
        private TunStageContainer db = new TunStageContainer();

        // GET: /Stages/
        public ActionResult Index()
        {
            ViewBag.mystages = new SelectList(db.StagesSet, "Id", "Type");
            if (User.IsInRole("admin") || User.IsInRole("etudiant"))
            {
                var stagesset = db.StagesSet.Include(s => s.Users);
                return View(stagesset.ToList());
            }
            else
            {
                if (User.IsInRole("recruteur"))
                {
                    string sess_id = Session["Username"].ToString();
                    int app = (from a in db.UsersSet
                               where a.Username == sess_id
                               select a.Id).First();
                    var stagesset = (from b in db.StagesSet.Include(s => s.Users)
                                     where b.UsersId == app
                                     select b);
                    return View(stagesset.ToList());
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        // GET: /Stages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stages stages = db.StagesSet.Find(id);
            if (stages == null)
            {
                return HttpNotFound();
            }
            Session["SelectedStage"] = id;

            return View(stages);
        }

        // GET: /Stages/Create
        public ActionResult Create()
        {
            ViewBag.UsersId = new SelectList(db.UsersSet, "Id", "Username");
            ViewBag.EntrepriseId = new SelectList(db.EntrepriseSet, "Id", "Designation");
            return View();
        }

        // POST: /Stages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Titre,Lieu,Description,Duree,Type,UsersId,EntrepriseId")] Stages stages)
        {
            if (ModelState.IsValid)
            {

                //DEBUT : recuperation de l'id de l'utilisateur
                string sess_id = Session["Username"].ToString();
               int stid = (from a in db.UsersSet
                                  where a.Username == sess_id
                                  select a.Id).First();
               stages.UsersId = stid;

                stages.EntrepriseId = (from a in db.EntrepriseSet.Include(s => s.Users)
                                  where a.UsersId == stid
                                  select a.Id).First();
                //FIN : recuperation de l'id de l'utilisateur

                db.StagesSet.Add(stages);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // ViewBag.UsersId = new SelectList(db.UsersSet, "Id", "Username", stages.UsersId);
            return View(stages);
        }

        // GET: /Stages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stages stages = db.StagesSet.Find(id);
            if (stages == null)
            {
                return HttpNotFound();
            }
            ViewBag.UsersId = new SelectList(db.UsersSet, "Id", "Username", stages.UsersId);
            ViewBag.EntrepriseId = new SelectList(db.EntrepriseSet, "Id", "Designation", stages.EntrepriseId);
            return View(stages);
        }

        // POST: /Stages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Titre,Lieu,Description,Duree,Type,UsersId,EntrepriseId")] Stages stages)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stages).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UsersId = new SelectList(db.UsersSet, "Id", "Username", stages.UsersId);
            ViewBag.EntrepriseId = new SelectList(db.EntrepriseSet, "Id", "Designation", stages.EntrepriseId);
            return View(stages);
        }

        // GET: /Stages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stages stages = db.StagesSet.Find(id);
            if (stages == null)
            {
                return HttpNotFound();
            }
            return View(stages);
        }

        // POST: /Stages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stages stages = db.StagesSet.Find(id);
            db.StagesSet.Remove(stages);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Recherche(string mot , string types)
        {
            ViewBag.mystages = new SelectList(db.StagesSet, "Id", "Types");
            var stages = from s in db.StagesSet
                         select s;
            if (!String.IsNullOrEmpty(mot))
            {
                stages = stages.Where(c => c.Titre.Contains(mot));
                if (!String.IsNullOrEmpty(types))
                {
                    stages = stages.Where(c => c.Type.Contains(types));
                }
            }
            else {
                 if (!String.IsNullOrEmpty(types)) { 
                 stages = stages.Where(c => c.Type.Contains(types));
                 }
            }
            return View(stages.ToList());
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
