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

        // GET: Likes/Create
        public ActionResult Create(int ?id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonLikesModel model = new PersonLikesModel();
            model.PersonId = id.Value;
            ViewBag.Id = id.Value;
            return View(model);
        }

        // POST: Likes/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,PersonId,CategoryId,Level")] PersonLikesModel personLikesModel)
        {
            int personId = personLikesModel.PersonId;
            if (ModelState.IsValid)
            {
                db.PersonLikes.Add(personLikesModel);
                db.SaveChanges();
                return RedirectToAction("Details","Persons", new { id = personId });
            }

            return View(personLikesModel);
        }
        public ActionResult CreateDislike(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonDislikesModel model = new PersonDislikesModel();
            model.PersonId = id.Value;
            ViewBag.Id = id.Value;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDisLike([Bind(Include = "Name,PersonId,CategoryId,Level")] PersonDislikesModel personDislikesModel)
        {
            int personId = personDislikesModel.PersonId;
            if (ModelState.IsValid)
            {
                db.PersonDislikes.Add(personDislikesModel);
                db.SaveChanges();
                return RedirectToAction("Details","Persons", new { id = personId });
            }

            return View(personDislikesModel);
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
            ViewBag.Id = id.Value;
            return View(personLikesModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,PersonId,CategoryId,Level")] PersonLikesModel personLikesModel)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(personLikesModel).State = EntityState.Modified;
                int personId = personLikesModel.PersonId;
                db.SaveChanges();
                return RedirectToAction("Details","Persons",new {id = personId });
            }
            return View(personLikesModel);
        }

        public ActionResult EditDislike(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonDislikesModel personDislikesModel = db.PersonDislikes.Find(id);
            if (personDislikesModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = id.Value;
            return View(personDislikesModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDislike([Bind(Include = "Id,Name,PersonId,CategoryId,Level")] PersonDislikesModel personDislikesModel)
        {

            if (ModelState.IsValid)
            {
                db.Entry(personDislikesModel).State = EntityState.Modified;
                int personId = personDislikesModel.PersonId;
                db.SaveChanges();
                return RedirectToAction("Details", "Persons", new { id = personId });
            }
            return View(personDislikesModel);
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
            ViewBag.Id = id.Value;

            return View(personLikesModel);
        }

        // POST: Likes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonLikesModel personLikesModel = db.PersonLikes.Find(id);
            int personId = personLikesModel.PersonId;
            db.PersonLikes.Remove(personLikesModel);
            db.SaveChanges();
            return RedirectToAction("Details", "Persons", new { id = personId });
        }

        public ActionResult DeleteDislike(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonDislikesModel personDislikesModel = db.PersonDislikes.Find(id);
            if (personDislikesModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = id.Value;
            return View(personDislikesModel);
        }

        // POST: Likes/Delete/5
        [HttpPost, ActionName("DeleteDislike")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDisLikeConfirmed(int id)
        {
            PersonDislikesModel personDislikesModel = db.PersonDislikes.Find(id);
            int personId = personDislikesModel.PersonId;
            db.PersonDislikes.Remove(personDislikesModel);
            db.SaveChanges();
            return RedirectToAction("Details", "Persons", new { id = personId });
        }

        [ChildActionOnly]
        public ActionResult LikesTable(int personId)
        {
            ViewBag.id = personId;
            return PartialView(GetPersonLikes(personId));
        }
        [ChildActionOnly]
        public ActionResult DislikesTable(int personId)
        {
            ViewBag.id = personId;
            return PartialView(GetPersonDislikes(personId));
        }


        private IEnumerable<Gifter.Models.PersonLikesDisplay> GetPersonLikes(int id)
        {
            IEnumerable<Gifter.Models.PersonLikesDisplay> display = (from l in db.PersonLikes.ToList().Where(like => like.PersonId == id)
                                                                     join c in db.Categories.ToList() on l.CategoryId equals c.Id
                                                                     select new PersonLikesDisplay
                                                                     {
                                                                         Id = l.Id,
                                                                         PersonId = l.PersonId,
                                                                         CategoryName = c.Name,
                                                                         Level = l.Level,
                                                                         Name = l.Name
                                                                     }).ToList();
            return display;
        }
        private IEnumerable<Gifter.Models.PersonDislikesDisplay> GetPersonDislikes(int id)
        {
            IEnumerable<Gifter.Models.PersonDislikesDisplay> display = (from d in db.PersonDislikes.ToList().Where(dislike => dislike.PersonId == id)
                                                                        join c in db.Categories.ToList() on d.CategoryId equals c.Id
                                                                        select new PersonDislikesDisplay
                                                                        {
                                                                            Id = d.Id,
                                                                            CategoryName = c.Name,
                                                                            Level = d.Level,
                                                                            Name = d.Name
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
