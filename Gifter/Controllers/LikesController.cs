using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gifter.Data;
using Gifter.Models;

namespace Gifter.Controllers
{
    public class LikesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Likes
        public ActionResult Index()
        {
            return View(db.PersonLikes.ToList());
        }
        

        // GET: Likes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonLikesModel personLikesModel = db.PersonLikes.Find(id);
            if (personLikesModel == null)
            {
                return HttpNotFound();
            }
            return View(personLikesModel);
        }

        // GET: Likes/Create
        public ActionResult Create(int id)
        {
            ViewBag.PersonId = id;
            return View();
        }

        // POST: Likes/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,PersonId,CategoryId,Level")] PersonLikesModel personLikesModel)
        {
            if (ModelState.IsValid)
            {
                db.PersonLikes.Add(personLikesModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(personLikesModel);
        }

        // GET: Likes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonLikesModel personLikesModel = db.PersonLikes.Find(id);
            if (personLikesModel == null)
            {
                return HttpNotFound();
            }
            return View(personLikesModel);
        }

        // POST: Likes/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,PersonId,CategoryId,Level")] PersonLikesModel personLikesModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personLikesModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personLikesModel);
        }

        // GET: Likes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonLikesModel personLikesModel = db.PersonLikes.Find(id);
            if (personLikesModel == null)
            {
                return HttpNotFound();
            }
            return View(personLikesModel);
        }

        // POST: Likes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonLikesModel personLikesModel = db.PersonLikes.Find(id);
            db.PersonLikes.Remove(personLikesModel);
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
