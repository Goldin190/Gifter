using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gifter.Models
{
    [Table("Presents",Schema = "public")]
    public class PresentsModel
    {
        [Key]
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string LinkToProduct { get; set; }
        public bool IsDone { get; set; }

    }

    [NotMapped]
    public class PresentsModelDisplay
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string CategoryName { get; set; }
        public string Name { get; set; }
        public string LinkToProduct { get; set; }
        public bool IsDone { get; set; }
    }
}