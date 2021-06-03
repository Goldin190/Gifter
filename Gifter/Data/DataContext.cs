using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Gifter.Models;

namespace Gifter.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base(nameOrConnectionString: "DataContext")
        {
        }

        public System.Data.Entity.DbSet<PersonModel> Persons { get; set; }
        public System.Data.Entity.DbSet<PersonProperyModel> PersonProperties { get; set; }
        public System.Data.Entity.DbSet<PersonLikesModel> PersonLikes{ get; set; }
        public System.Data.Entity.DbSet<PersonDislikesModel> PersonDislikes { get; set; }
        public System.Data.Entity.DbSet<PresentsModel> Presents { get; set; }
        public System.Data.Entity.DbSet<CategoriesModel> Categories { get; set; }

    }
}