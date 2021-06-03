using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Permissions;
using System.Web.Services.Description;

namespace Gifter.Models
{
    [Table("Persons",Schema = "public")]
    public class PersonModel
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SirName { get; set; }
        public string AddInfo { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
    }
    [Table("PersonsProperties", Schema = "public")]
    public class PersonProperyModel
    {
        [Key]
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
    [Table("PersonsLikes", Schema = "public")]
    public class PersonLikesModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int PersonId { get; set; }
        public int CategoryId { get; set; }
        public int Level { get; set; }
    }
    [Table("PersonsDislikes", Schema = "public")]
    public class PersonDislikesModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int PersonId { get; set; }
        public int CategoryId { get; set; }
        public int Level { get; set; }
    }
    [Table("PropertiesCategories", Schema = "public")]
    public class PropertiesCategoryModel
    {
        [Key]
        public int Id{ get; set; }
        public string Name { get; set; }
    }
    [Table("DislikesAndLikesCategory",Schema = "public")]
    public class DislikesAndLikesCategoryModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
    }
    [NotMapped]
    public class PersonDetailsModel : PersonModel
    {
        public List<PersonLikesModel> personLikes;
        public List<PersonDislikesModelDisplay> personDislikes;
        public List<PersonProperyModel> personProperies;
    }
    [NotMapped]
    public class PersonDislikesModelDisplay : PersonDislikesModel
    {
        public string CategoryName { get; set; }
        public string CateogryValue { get; set; }
    }
}