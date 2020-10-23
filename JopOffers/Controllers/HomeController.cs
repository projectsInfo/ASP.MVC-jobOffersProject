using JopOffers.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;


namespace JopOffers.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {

            return View(db.Categories.ToList());
        }

        public ActionResult Details(int jobId)
        {

            var job = db.Jobs.Find(jobId);
            if (job == null)
            {

                return HttpNotFound();
            }
            Session["JobId"] = jobId;
            return View(job);
        }
        [Authorize]
        public ActionResult Apply()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Apply(String Message)
        {

            var UserId = User.Identity.GetUserId();
            var JobId = (int)Session["JobId"];

            var Check = db.ApplyForJobs.Where(a => a.JobId == JobId && a.UserId == UserId).ToList();
            if (Check.Count < 1)
            {

                var job = new ApplyForJob();
                job.UserId = UserId;
                job.JobId = JobId;
                job.Message = Message;
                job.ApplyDate = DateTime.Now;

                db.ApplyForJobs.Add(job);
                db.SaveChanges();
                ViewBag.Result = "Your Add Done!";
            }
            else
            {
                ViewBag.Result = "Sorry. You Choose this Job From Before!";
            }

            return View();
        }
        [Authorize]
        public ActionResult GetJobsByUser()
        {

            var UserId = User.Identity.GetUserId();
            var Jobs = db.ApplyForJobs.Where(a => a.UserId == UserId);
            return View(Jobs.ToList());
        }
        [Authorize]
        public ActionResult DetailsOfJobs(int Id)
        {

            var job = db.ApplyForJobs.Find(Id);
            if (job == null)
            {

                return HttpNotFound();
            }

            return View(job);
        }

        [Authorize]
        public ActionResult GetJobsByPublisher()
        {

            var UserId = User.Identity.GetUserId();

            var Jobs = from app in db.ApplyForJobs
                       join job in db.Jobs
                       on app.JobId equals job.Id
                       where job.User.Id == UserId
                       select app;

            var grouped = from j in Jobs
                          group j by j.job.JobTitle
                          into gr
                          select new JobsViewModel
                          {
                              JobTitle = gr.Key,
                              Items = gr
                          };

            return View(grouped.ToList());
        }

        public ActionResult Edit(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        [HttpPost]
        public ActionResult Edit(ApplyForJob job)
        {
            if (ModelState.IsValid)
            {
                job.ApplyDate = DateTime.Now;
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetJobsByUser");
            }
            return View(job);
        }

        public ActionResult Delete(int Id)
        {
            var job = db.ApplyForJobs.Find(Id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        [HttpPost]
        public ActionResult Delete(ApplyForJob job)
        {
            try
            {
                // TODO: Add delete logic here
                var MyJob = db.ApplyForJobs.Find(job.Id);
                db.ApplyForJobs.Remove(MyJob);
                db.SaveChanges();
                return RedirectToAction("GetJobsByUser");
            }
            catch
            {
                return View(job);
            }
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
            var loginInfo = new NetworkCredential("Mail@Example.com", "Set Password");
            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("Mail@Example.com"));
            mail.Subject = contact.SubJect;
            mail.IsBodyHtml = true;
            String body = "Sender Name:" + contact.Name + "<br>" +
                            "Sender Mail: " + contact.Email + "<br>" +
                            "Message Title: " + contact.SubJect + "<br>" +
                            "Message Supject: <b>" + contact.Message + "</b>";

            mail.Body = body;

            // Send Email
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
        public ActionResult Search(String searchName)
        {
            var result = db.Jobs.Where(a => a.JobTitle.Contains(searchName)
            || a.JobContent.Contains(searchName)
            || a.Category.CategoryName.Contains(searchName)
            || a.Category.CategoryDescription.Contains(searchName)).ToList();
            return View(result);
        }

    }
}