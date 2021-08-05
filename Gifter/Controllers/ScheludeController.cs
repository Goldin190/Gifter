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
    public class ScheludeController : Controller
    {
        // GET: Schelude
        private DataContext db = new DataContext();
        private List<int> DaysInMonth = new List<int> { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
        public ActionResult Index()
        {
            string id = User.Identity.GetUserId();
            return View(SortByBirthDay(db.Persons.Where(p => p.UserId == id).OrderBy(p => p.BirthDate)).ToList());
        }
        private IEnumerable<PersonModel> SortByBirthDay(IEnumerable<PersonModel> personModels)
        {
            DateTime Now = DateTime.Now;
            List<PersonModel> result = new List<PersonModel>();
            result.AddRange(personModels.Where(p => p.BirthDate >= Now).OrderBy(p => p.BirthDate));
            result.AddRange(personModels.Where(p => p.BirthDate < Now).OrderBy(p => p.BirthDate));
            return result;
        }
        private int GetIntFromDayOfWeek(string dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case "Monday":
                    return 1;
                    
                case "Tuesday":
                    return 2;
                    
                case "Wednesday":
                    return 3;
                    
                case "Thursday":
                    return 4;
                    
                case "Friday":
                    return 5;
                    
                case "Saturday":
                    return 6;
                    
                case "Sunday":
                    return 7;
                    
                default:
                    return 0;
                    
            }
        }
    }
}