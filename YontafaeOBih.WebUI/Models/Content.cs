using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace YontafaeOBih.WebUI.Models
{
    public class Content
    {
        public int Id { get; set; }
        [DisplayName("اسم المحتوى")]
        public string ContentTitle { get; set; }
        [DisplayName("وصف المحتوى")]
        [AllowHtml]
        public string ContentDescription { get; set; }
        [DisplayName("صورة معبرة عن المحتوى")]
        public string ContentImage { get; set; }

        [DisplayName("نوع المحتوى")]
        public int CategoryId { get; set; }
        public string UserId { get; set; }


        public virtual Category Category { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}