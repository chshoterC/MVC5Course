using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        private FabricsEntities db = new FabricsEntities();


        public ActionResult Index()
         {
             var all = db.Product.AsQueryable();
 
             var data = all
                 .Where(p => p.ProductName.Contains("Black"))
                 .OrderByDescending(p => p.ProductId)
                 .Take(300);
 
             return View(data);
         }

    public ActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int? id)
        {
            var data = db.Product.Find(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            if(ModelState.IsValid)
            {
                var data = db.Product.Find(id);
                data.ProductName = product.ProductName;
                data.Active = product.Active;
                data.Price = product.Price;
                data.Stock = product.Stock;
                db.SaveChanges();

                return RedirectToAction("Index");

            }
            return View(product);
        }

        public ActionResult Delete(int id)
        {
            var pd = db.Product.Find(id);

            //foreach( var item in pd.OrderLine)
            //{
            //    db.OrderLine.Remove(item);
            //}

            db.OrderLine.RemoveRange(pd.OrderLine);

            db.Product.Remove(pd);



            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var pd = db.Product.Find(id);

            return View(pd);
        }
    }
}