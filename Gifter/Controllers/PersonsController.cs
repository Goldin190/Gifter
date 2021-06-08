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
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Security.Cryptography;

namespace Gifter.Controllers
{
    public class PersonsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Persons
        public ActionResult Index()
        {
            return View(db.Persons.ToList().Where(p => p.UserId == User.Identity.GetUserId()));
        }

        // GET: Persons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonModel personModel = db.Persons.Find(id);
            if (personModel == null)
            {
                return HttpNotFound();
            }
            return View(personModel);
        }

        // GET: Persons/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Persons/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,AddInfo,BirthDate,FirstName,SirName")] PersonModel personModel)
        {
            if (ModelState.IsValid)
            {
                personModel.UserId =  User.Identity.GetUserId();
                db.Persons.Add(personModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(personModel);
        }
        public ActionResult CreateLike(int ?id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonLikesModel model = new PersonLikesModel();
            model.PersonId = id.Value;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,PersonId,CategoryId,Level")] PersonLikesModel personLikesModel)
        {
            if (ModelState.IsValid)
            {
                db.PersonLikes.Add(personLikesModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(personLikesModel);
        }

        // GET: Persons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonModel personModel = db.Persons.Find(id);
            if (personModel == null)
            {
                return HttpNotFound();
            }
            return View(personModel);
        }

        // POST: Persons/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,AddInfo,BirthDate")] PersonModel personModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personModel);
        }

        // GET: Persons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonModel personModel = db.Persons.Find(id);
            if (personModel == null)
            {
                return HttpNotFound();
            }
            return View(personModel);
        }

        // POST: Persons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonModel personModel = db.Persons.Find(id);
            db.Persons.Remove(personModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        public ActionResult PropertiesTable(int personId)
        {
            return PartialView(GetPersonProperies(personId));
        }

        [ChildActionOnly]
        public ActionResult DislikesTable(int personId) 
        {
            return PartialView(GetPersonDislikes(personId));
        }

        [ChildActionOnly]
        public ActionResult LikesTable(int personId)
        {
            return PartialView(GetPersonLikes(personId));
        }

        [ChildActionOnly]
        public ActionResult PresentsTable(int personId)
        {
            return PartialView(GetPresents(personId));
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



        private IEnumerable<Gifter.Models.PersonProperyModel> GetPersonProperies(int id)
        {
           return db.PersonProperties.Where(p => p.PersonId == id).ToList();
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
