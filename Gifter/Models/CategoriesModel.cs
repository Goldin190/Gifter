using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gifter.Models
{
    [Table("Categories",Schema = "public")]
    public class CategoriesModel
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }

    }
}