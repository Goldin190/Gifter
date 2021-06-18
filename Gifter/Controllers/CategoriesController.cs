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
using Microsoft.AspNet.Identity;

namespace Gifter.Controllers
{
    public class CategoriesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Categories
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Categories.ToList().Where(c => c.UserId == User.Identity.GetUserId() || c.UserId == "All"));   
        }
        [ChildActionOnly]
        public ActionResult DropDown(int ?id)
        {
            if(id == null)
            {
                ViewBag.id = 0;
            }
            else
            {
                ViewBag.id = id.Value;
            }
            return View(GetCategories());
        }

        private IEnumerable<CategoriesDropDown> GetCategories()
        {
            string userId = User.Identity.GetUserId();
            IEnumerable<CategoriesDropDown> result = (from c in db.Categories
                                               where c.UserId == userId || c.UserId == "All"
                                               select new CategoriesDropDown
                                               {
                                                   Id = c.Id,
                                                   Name = c.Name
                                               }).ToList();
            return result;
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,UserId")] CategoriesModel categoriesModel)
        {
            if (ModelState.IsValid)
            {
                categoriesModel.UserId = User.Identity.GetUserId();
                db.Categories.Add(categoriesModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoriesModel);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriesModel categoriesModel = db.Categories.Find(id);
            if (categoriesModel == null)
            {
                return HttpNotFound();
            }
            return View(categoriesModel);
        }

        // POST: Categories/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name,UserId")] CategoriesModel categoriesModel)
        {
            if (ModelState.IsValid)
            {
                categoriesModel.UserId = User.Identity.GetUserId();
                db.Entry(categoriesModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoriesModel);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriesModel categoriesModel = db.Categories.Find(id);
            if (categoriesModel == null)
            {
                return HttpNotFound();
            }
            return View(categoriesModel);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoriesModel categoriesModel = db.Categories.Find(id);
            db.Categories.Remove(categoriesModel);
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
