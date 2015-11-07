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
    public class CartItemsController : Controller
    {
        private OnlineStoreEntities db = new OnlineStoreEntities();

        // GET: CartItems
        public ActionResult Index()
        {
      var db = new DB();
      var cart = new ShoppingCart();
      var cartItem = cart.GetCartItems();
            return View(cartItem.ToList());
        }

    // GET: CartItems/Details/5
    public ActionResult Details(string id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      CartItem cartItem = db.CartItems.Find(id);
      if (cartItem == null)
      {
        return HttpNotFound();
      }
      return View(cartItem);
    }
     
        // GET: CartItems/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        // POST: CartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CartItem cartItem = db.CartItems.Find(id);
            db.CartItems.Remove(cartItem);
            db.SaveChanges();
            return RedirectToAction("Index");
    }
    public ActionResult AddToCart(int id)
    {
      var db = new DB();
      var cart = new ShoppingCart();
      cart.AddToCart(id);
      var cartItem = cart.GetCartItems();
      return View(cartItem);
    }

    public ActionResult Checkout()
    {

      var db = new DB();
      var cart = new ShoppingCart();
      var cartItem = cart.GetCartItems();

      if (Session["InnLogget"] != null)
      {
        Session["InnLogget"] = true;
        ViewBag.Loggetinn = true;
        return View(cartItem.ToList());
      }
      else
      {
        Session["InnLogget"] = false;
        ViewBag.Loggetinn = false;
        return RedirectToAction("../Sikkerhet/Index");
      }

    }

    public ActionResult Kvittering()
    {
      var db = new DB();
      var cart = new ShoppingCart();
      var cartItem = cart.GetCartItems();

      if (Session["InnLogget"] != null)
      {
        Session["InnLogget"] = true;
        ViewBag.Loggetinn = true;
        return View(cartItem.ToList());
      }
      else
      {
        Session["InnLogget"] = false;
        ViewBag.Loggetinn = false;
        return RedirectToAction("../Sikkerhet/Index");
      }
    }
  

    public ActionResult Kvittering()
    {
            var db = new DB();
            var cart = new ShoppingCart();
            var cartItem = cart.GetCartItems();

            if (Session["InnLogget"] != null)
            {
                Session["InnLogget"] = true;
                ViewBag.Loggetinn = true;
                return View(cartItem.ToList());
            }
            else
            {
                Session["InnLogget"] = false;
                ViewBag.Loggetinn = false;
                return RedirectToAction("../Sikkerhet/Index");
            }
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
