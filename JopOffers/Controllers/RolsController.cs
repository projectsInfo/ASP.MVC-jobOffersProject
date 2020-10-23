using JopOffers.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JopOffers.Controllers
{
    [Authorize(Roles = "Admins")]
    public class RolsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Rols
        public ActionResult Index()
        {
            return View(db.Roles.ToList());
        }

        // GET: Rols/Details/5
        public ActionResult Details(String id)
        {
            var role = db.Roles.Find(id);
            if (role == null) {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: Rols/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rols/Create
        [HttpPost]
        public ActionResult Create(IdentityRole role)
        {
                // TODO: Add insert logic here
                if (ModelState.IsValid) {
                    db.Roles.Add(role);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(role);
        }

        // GET: Rols/Edit/5
        public ActionResult Edit(String id)
        {
            var role = db.Roles.Find(id);
            if (role == null) {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Rols/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include ="Id,Name")]IdentityRole role)
        {
            if (ModelState.IsValid) {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: Rols/Delete/5
        public ActionResult Delete(String id)
        {
            var role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);

        }

        // POST: Rols/Delete/5
        [HttpPost]
        public ActionResult Delete(IdentityRole role)
        {
            try
            {
                // TODO: Add delete logic here
                var myrole = db.Roles.Find(role.Id);
                db.Roles.Remove(myrole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(role);
            }
        }
    }
}
