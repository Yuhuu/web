using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using webshop.Models;
using webshop.Logic;
using System;

namespace webshop.Controllers
{
    public class StoreController : Controller
    {

        public ActionResult Index()
        {
         var db = new DB();
         List<Vare> alleBestillinger = db.listAlleVare();
         return View(alleBestillinger);
        }
  }
}
