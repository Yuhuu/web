using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using webshop.Models;
using webshop.Logic;

namespace webshop.Controllers
{
    public class CustomersController : Controller
    {
        private OnlineStoreEntities db = new OnlineStoreEntities();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Kunder.ToList());
        }
        public ActionResult SignIn()
        {
          return View();
        }
    // GET: Customers/Details/5
    public ActionResult Details(int? id = 0)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kunde kunde = db.Kunder.Find(id);
            if (kunde == null)
            {
                return HttpNotFound();
            }
            return View(kunde);
        }

        // GET: Customers/Create
    //    public ActionResult Create(Kunde best)
    //{
    //    var db = new DB();
    //  if (ModelState.IsValid)
    //  {
    //    if (db.SettInnNyKunde(best))
    //    {
    //      return RedirectToAction("Index");
    //    }
    //    return View();
    //  }
    //}

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "KId,Navn,Adresse,Epost,Password")] Kunde kunde)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Kunder.Add(kunde);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(kunde);
        //}

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kunde kunde = db.Kunder.Find(id);
            if (kunde == null)
            {
                return HttpNotFound();
            }
            return View(kunde);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KId,Navn,Adresse,Epost,Password")] Kunde kunde)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kunde).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kunde);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kunde kunde = db.Kunder.Find(id);
            if (kunde == null)
            {
                return HttpNotFound();
            }
            return View(kunde);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kunde kunde = db.Kunder.Find(id);
            db.Kunder.Remove(kunde);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
