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
    public class CarteController : Controller
    {
        private Model1Container db = new Model1Container();

        // GET: /Carte/
        public ActionResult Index()
        {
            //var carteset = db.CarteSet.Include(c => c.UsersSet);
            var path1 = (from a in db.CarteSet
                         select a).ToList();

            //string path = Server.MapPath("~/images/randonne.jpg");
            //byte[] imageByteData = System.IO.File.ReadAllBytes(path1);
            //string imageBase64Data = Convert.ToBase64String(imageByteData);
            //string imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
            //ViewBag.ImageData = imageDataURL;
            return View(path1);

        }

        public ActionResult Gallery()
        {
            return View(db.CarteSet.ToList());

        }

        // GET: /Carte/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarteSet carteset = db.CarteSet.Find(id);
            if (carteset == null)
            {
                return HttpNotFound();
            }
            return View(carteset);
        }

        // GET: /Carte/Create
        public ActionResult Create()
        {
            ViewBag.UsersId = new SelectList(db.UsersSet, "Id", "Username");
            return View();
        }

        // POST: /Carte/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Gouvernerat,Type,UsersId")] CarteSet carteset)
        {
            if (ModelState.IsValid)
            {
                db.CarteSet.Add(carteset);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UsersId = new SelectList(db.UsersSet, "Id", "Username", carteset.UsersId);
            return View(carteset);
        }

        // GET: /Carte/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarteSet carteset = db.CarteSet.Find(id);
            if (carteset == null)
            {
                return HttpNotFound();
            }
            ViewBag.UsersId = new SelectList(db.UsersSet, "Id", "Username", carteset.UsersId);
            return View(carteset);
        }

        // POST: /Carte/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Designation,Path,UsersId")] CarteSet carteset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carteset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UsersId = new SelectList(db.UsersSet, "Id", "Username", carteset.UsersId);
            return View(carteset);
        }

        // GET: /Carte/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarteSet carteset = db.CarteSet.Find(id);
            if (carteset == null)
            {
                return HttpNotFound();
            }
            return View(carteset);
        }

        // POST: /Carte/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarteSet carteset = db.CarteSet.Find(id);
            db.CarteSet.Remove(carteset);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            CarteSet c = new CarteSet();
            return View(c);
        }
        [HttpPost]
        public ActionResult FileUpload(CarteSet c, HttpPostedFileBase file)
        {
            string sess_id = Session["Username"].ToString();
            int app = (from a in db.UsersSet
                       where a.Username == sess_id
                       select a.Id).First();
            if (file != null)
            {

                c.Designation = Request.Form["Designation"];

                c.Path = new byte[file.ContentLength];
                file.InputStream.Read(c.Path, 0, file.ContentLength);
                c.UsersId = app;



            }
            db.CarteSet.Add(c);
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
