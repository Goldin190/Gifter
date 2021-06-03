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
            return View(db.Persons.ToList());
        }


        // GET: Persons/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonModel personModel = db.Persons.Find(id);
            var model = (from p in db.Persons.ToList()
                         join l in db.PersonLikes.ToList() on p.Id equals l.PersonId
                         join d in db.PersonDislikes.ToList() on p.Id equals d.PersonId
                         join pr in db.PersonProperties.ToList() on p.Id equals pr.PersonId
                         join pc in db.propertiesCategories.ToList() on pr.CategoryId equals pc.Id
                         where p.Id == id
                         select new PersonDetailsModel
                         {
                         }
                        );
            PersonDetailsModel personDetails = (from p in db.Persons.ToList()
                                                where p.Id == id
                                                select new PersonDetailsModel
                                                {
                                                    Id = p.Id,
                                                    UserId = p.UserId,
                                                    FirstName = p.FirstName,
                                                    SirName = p.SirName,
                                                    AddInfo = p.AddInfo,
                                                    BirthDate = p.BirthDate
                                                }).First();
            //personDetails.personDislikes = db.PersonDislikes.Where(d => d.PersonId == personDetails.Id).ToList().Join(db.);
            //Zmienić personDislikes na personDislikesDisplay - > catID -> catname,catval;
            personDetails.personLikes = db.PersonLikes.Where(d => d.PersonId == personDetails.Id).ToList();
            personDetails.personProperies = new List<PersonProperyModel>();

            


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
