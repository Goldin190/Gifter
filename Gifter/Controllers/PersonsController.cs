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
            return View(db.Persons.ToList().Where(p => p.UserId == User.Identity.GetUserId()).OrderBy(p => p.Id));
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
        public ActionResult CreateProperty(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonProperyModel model = new PersonProperyModel();
            model.PersonId = id.Value;
            ViewBag.id = id;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProperty([Bind(Include = "PersonId,Name,Value")] PersonProperyModel personProperyModel)
        {
            int personId = personProperyModel.PersonId;
            if (ModelState.IsValid)
            {
                db.PersonProperties.Add(personProperyModel);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = personId });
            }

            return View(personProperyModel);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,SirName,UserId,AddInfo,BirthDate")] PersonModel personModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personModel);
        }

        public ActionResult EditProperty(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonProperyModel personProperyModel = db.PersonProperties.Find(id);
            if (personProperyModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = personProperyModel.PersonId;
            return View(personProperyModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProperty([Bind(Include = "Id,PersonId,Name,Value")] PersonProperyModel personProperyModel)
        {
            int personId = personProperyModel.PersonId;
            if (ModelState.IsValid)
            {
                db.Entry(personProperyModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details","Persons",new { id = personId });
            }
            return View(personProperyModel);
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
        public ActionResult DeleteProperty(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonProperyModel personProperyModel= db.PersonProperties.Find(id);
            if (personProperyModel == null)
            {
                return HttpNotFound();
            }
            return View(personProperyModel);
        }

        // POST: Persons/Delete/5
        [HttpPost, ActionName("DeleteProperty")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePropertyConfirmed(int id)
        {
            PersonProperyModel personProperyModel = db.PersonProperties.Find(id);
            int personId = personProperyModel.PersonId;
            db.PersonProperties.Remove(personProperyModel);
            db.SaveChanges();
            return RedirectToAction("Details",new { id = personId });
        }

        [ChildActionOnly]
        public ActionResult PropertiesTable(int personId)
        {
            ViewBag.id = personId;
            return PartialView(GetPersonProperies(personId));
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
