using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using webshop.Logic;
using webshop.Models;

namespace webshop.Controllers
{
  public class ProduktController : Controller
    {
    private OnlineStoreEntities db = new OnlineStoreEntities();
    public ActionResult Index()
        {
            List<Vare> alleBestillinger = db.Vareer.ToList();
            return View(alleBestillinger);
        }

    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Vare vare = db.Vareer.Find(id);
      if (vare == null)
      {
        return HttpNotFound();
      }
      ViewBag.VareProduktMerke = new SelectList(db.Vareer, "ProduktMerke", "ProduktMerke", vare.ProduktMerke);
      return View(vare);
    }

    // POST: ShoppingCart/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit
      ([Bind(Include = "VareId,ProduktNavn,VareProduktMerke,Pris,Antall")] Vare vare)
    {
      var db = new OnlineStoreEntities();
      if (ModelState.IsValid)
      {
        db.Entry(vare).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      ViewBag.VareId = new SelectList(db.Vareer, "ProduktMerke", "ProduktMerke", vare.ProduktMerke);
      return View(vare);
    }

    public ActionResult NyVare()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NyVare(Vare best)
        {
            var db = new DB();
            if(db.SettInnNyVare(best))
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}