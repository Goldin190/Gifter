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
    public class PresentsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Presents
        public ActionResult Index()
        {
            return View(db.Presents.ToList());
        }

        // GET: Presents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PresentsModel presentsModel = db.Presents.Find(id);
            if (presentsModel == null)
            {
                return HttpNotFound();
            }
            return View(presentsModel);
        }

        // GET: Presents/Create
        public ActionResult Create(int ?id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PresentsModel present = new PresentsModel();
            present.PersonId = id.Value;
            ViewBag.id = id.Value;
            return View(present);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PersonId,CategoryId,Name,LinkToProduct,IsDone")] PresentsModel presentsModel)
        {
            int personId = presentsModel.PersonId;
            if (ModelState.IsValid)
            {
                db.Presents.Add(presentsModel);
                db.SaveChanges();
                return RedirectToAction("Details", "Persons", new { id = personId });
            }

            return View(presentsModel);
        }

        // GET: Presents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PresentsModel presentsModel = db.Presents.Find(id);
            if (presentsModel == null)
            {
                return HttpNotFound();
            }
            return View(presentsModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PersonId,CategoryId,Name,LinkToProduct,IsDone")] PresentsModel presentsModel)
        {
            int personId = presentsModel.PersonId;
            if (ModelState.IsValid)
            {
                db.Entry(presentsModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Persons", new { id = personId });
            }
            return View(presentsModel);
        }

        // GET: Presents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PresentsModel presentsModel = db.Presents.Find(id);
            if (presentsModel == null)
            {
                return HttpNotFound();
            }
            return View(presentsModel);
        }

        // POST: Presents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PresentsModel presentsModel = db.Presents.Find(id);
            db.Presents.Remove(presentsModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        public ActionResult PresentsTable(int personId)
        {
            ViewBag.id = personId;
            return PartialView(GetPresents(personId));
        }

        private IEnumerable<Gifter.Models.PresentsModelDisplay> GetPresents(int id)
        {
            IEnumerable<Gifter.Models.PresentsModelDisplay> display = (from p in db.Presents.Where(present => present.PersonId == id)
                                                                       join c in db.Categories on p.CategoryId equals c.Id
                                                                       select new PresentsModelDisplay
                                                                       {
                                                                           Id = p.Id,
                                                                           PersonId = p.PersonId,
                                                                           LinkToProduct = p.LinkToProduct,
                                                                           CategoryName = c.Name,
                                                                           Name = p.Name,
                                                                           IsDone = p.IsDone
                                                                       }).ToList();
            return display;
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
