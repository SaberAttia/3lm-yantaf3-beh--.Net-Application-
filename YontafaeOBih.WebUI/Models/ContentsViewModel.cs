using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YontafaeOBih.WebUI.Models
{
    public class ContentsViewModel
    {
        public string ContentTitle { get; set; }
        public IEnumerable<ApplyForThisContent> Items { get; set; }
    }
}