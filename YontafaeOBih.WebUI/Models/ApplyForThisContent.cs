using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace YontafaeOBih.WebUI.Models
{
    public class ApplyForThisContent
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime ApplyDate { get; set; }
        public int ContentId { get; set; }
        public string UserId { get; set; }

        public virtual Content content { get; set; }
        public virtual ApplicationUser user { get; set; }
    }
}