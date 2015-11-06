using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webshop.Models;
using System.Text;

namespace webshop.Controllers
{
    public class SikkerhetController : Controller
    {
        // GET: Sikkerhet
        public ActionResult Index()
        {
            if (Session["InnLogget"] == null)
            {
                Session["InnLogget"] = false;
                ViewBag.Loggetinn = false;
            }
            else
            {
                ViewBag.Loggetinn = (bool)Session["InnLogget"];
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(bruker innBruker)
        {
            if (Bruker_i_DB(innBruker))
            {
                Session["InnLogget"] = true;
                ViewBag.Loggetinn = true;
                return View();
            }
            else
            {
                Session["InnLogget"] = false;
                ViewBag.Loggetinn = false;
                return View();
            }            
        }

        public ActionResult Registrer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrer(bruker innBruker)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }                 
            using (var db = new BrukerContext())
            {
                try
                {
                    var nyBruker = new dbBruker();
                    byte[] passordDb = konvertTilHash(innBruker.Passord);
                    nyBruker.Passord = passordDb;
                    nyBruker.Navn = innBruker.Navn;
                    db.Brukere.Add(nyBruker);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch(Exception feil)
                {
                    return View();
                }
            }       
        }

        public static byte[] konvertTilHash(string innPassord)
        {
            byte[] innData, utData;
            var algoritme = System.Security.Cryptography.SHA256.Create();
            innData = System.Text.Encoding.ASCII.GetBytes(innPassord);
            utData = algoritme.ComputeHash(innData);
            return utData;
        }

        private static bool Bruker_i_DB(bruker innBruker)
        {
            using (var db = new BrukerContext())
            {
                byte[] passordDb = konvertTilHash(innBruker.Passord);
                dbBruker brukerOK = db.Brukere.FirstOrDefault(b => b.Passord == passordDb && b.Navn == innBruker.Navn);
                if(brukerOK == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public ActionResult LoggAv()
        {
            if (Session["InnLogget"] != null)
            {
                Session["InnLogget"] = null;
                ViewBag.Loggetinn = true;
            }
            else if (Session["InnLogget"] == null)
            {
                ViewBag.Loggetinn = (bool)Session["InnLogget"];
            }
            return View();
        }

        //public ActionResult LoggAv(bruker innBruker)
        //{
        //    if (Bruker_i_DB(innBruker))
        //    {
        //        Session["InnLogget"] = false;
        //        ViewBag.Loggetinn = false;
        //        return View();
        //    }                           
        //    else
        //    {
        //        return View();                                                                                
        //    }
        //}

        public ActionResult InnLoggetSide()
        {
            if(Session["InnLogget"] != null)
            {
                bool innLogget = (bool)Session["InnLogget"];
                if (innLogget)
                {
                    Session["InnLogget"] = null;
                    ViewBag.Loggetinn = false;
                    return View();
                }
            }
            return RedirectToAction("Index");
        }
    }
}