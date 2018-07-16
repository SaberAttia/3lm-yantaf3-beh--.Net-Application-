using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YontafaeOBih.WebUI.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="نوع المحتوى")]
        public string CategoryName { get; set; }
        [Required]
        [Display(Name = "وصف المحتوى")]
        public string CategoryDescription { get; set; }

        public virtual ICollection<Content> Contents { get; set; } 
    }
}