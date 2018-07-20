using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using YontafaeOBih.WebUI.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var list = db.Categories.ToList();
            return View(list);
        }

        public ActionResult Details(int ContentId)
        {
            var content = db.Contents.Find(ContentId);
            if (content == null)
            {
                return HttpNotFound();
            }
            Session["ContentId"] = ContentId;
            return View(content);
        }

        [Authorize]
        public ActionResult Apply() //Get
        {
            return View();
        }

        [HttpPost]
        public ActionResult Apply(string Message) //Post
        {
            var UserId = User.Identity.GetUserId();
            var ContentId = (int)Session["ContentId"];

            var check = db.ApplyForThisContents.Where(a=>a.ContentId==ContentId && a.UserId == UserId).ToList();
            if (check.Count < 1)
            {
                var content = new ApplyForThisContent();
                content.UserId = UserId;
                content.ContentId = ContentId;
                content.Message = Message;
                content.ApplyDate = DateTime.Now;

                db.ApplyForThisContents.Add(content);
                db.SaveChanges();
                ViewBag.Result = "تمت العملية بنجاح";
            }
            else
            {
                ViewBag.Result = "المعذرة, لقد سبق لك التسجيل في هذا المحتوى";
            }

            
             
            return View();
        }

        [Authorize]
        public ActionResult GetContentsByUser()
        {
            var UserId = User.Identity.GetUserId();
            var Contents = db.ApplyForThisContents.Where(a => a.UserId == UserId);
            return View(Contents.ToList());
        }
        [Authorize]
        public ActionResult DetailsOfContents(int id)
        {
            var content = db.ApplyForThisContents.Find(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            return View(content);
        }

        public ActionResult Edit(int id)
        {
            var content = db.ApplyForThisContents.Find(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            return View(content);
        }

        [HttpPost]
        public ActionResult Edit(ApplyForThisContent content)
        {
            if (ModelState.IsValid)
            {
                content.ApplyDate = DateTime.Now;
                db.Entry(content).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetContentsByUser");
            }
            return View(content);
        }

        public ActionResult Delete(int id)
        {
            var content = db.ApplyForThisContents.Find(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            return View(content);
        }

        
        [HttpPost]
        public ActionResult Delete(ApplyForThisContent content)
        {
            var myContent = db.ApplyForThisContents.Find(content.Id);
            db.ApplyForThisContents.Remove(myContent);
            db.SaveChanges();
            return RedirectToAction("GetContentsByUser");

        }

        [Authorize]
        public ActionResult GetContentsByPublisher()
        {
            var UserId = User.Identity.GetUserId();
            var Contents = from app in db.ApplyForThisContents
                           join content in db.Contents
                           on app.ContentId equals content.Id
                           where content.User.Id == UserId
                           select app;

            var grouped = from c in Contents
                          group c by c.content.ContentTitle
                          into gr
                          select new ContentsViewModel
                          {
                              ContentTitle = gr.Key,
                              Items = gr
                          };

            return View(grouped.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModel contact)
        {
            var mail = new MailMessage();
            var loginInfo = new NetworkCredential("saber100111@gmail.com", "00000000");
            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("saber100111@gmail.com"));
            mail.Subject = contact.Subject;
            mail.IsBodyHtml = true;
            string body = "اسم المرسل: " + contact.Name + "<br>" +
                            "بريد المرسل: " + contact.Email + "<br>" +
                            "عنوان الرسالة: " + contact.Subject + "<br>" +
                            "نص الرسالة: <b>" + contact.Message + "</b>";
            mail.Body = body;

            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(mail);
            return RedirectToAction("Index");
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var result = db.Contents.Where(a => a.ContentTitle.Contains(searchName)
            || a.ContentDescription.Contains(searchName)
            || a.Category.CategoryName.Contains(searchName)
            || a.Category.CategoryDescription.Contains(searchName)).ToList();
            return View(result);
        }
    }
}