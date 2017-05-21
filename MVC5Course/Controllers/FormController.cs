using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class FormController : BaseController
    {
        public ActionResult Index(double CreditRatingFilter = -1, string LastNameFilter = "")
        {

            var data = db.Client.AsQueryable();


            if (CreditRatingFilter > 0)
            {
                Session["CreditRatingFilter"] = CreditRatingFilter;
                data = data.Where(p => p.CreditRating == CreditRatingFilter);
            }
            else
            {
                if (Session["CreditRatingFilter"] != null && (double)Session["CreditRatingFilter"] > 0)
                {
                    data = data.Where(p => p.CreditRating == CreditRatingFilter);
                    CreditRatingFilter = (double)Session["CreditRatingFilter"];
                }
            }

            if (!string.IsNullOrEmpty(LastNameFilter))
            {
                Session["LastNameFilter"] = LastNameFilter;
                data = data.Where(p => p.LastName.Contains(LastNameFilter));
            }
            else
            {
                if (Session["LastNameFilter"] != null && Session["LastNameFilter"].ToString() != "")
                {
                    data = data.Where(p => p.LastName.Contains(LastNameFilter));
                    LastNameFilter = Session["LastNameFilter"].ToString();
                }
            }

            ViewData.Model = data.Take(20);


            var ratings = (from p in db.Client
                           select p.CreditRating)
                          .Distinct().OrderBy(p => p).ToList();

            ViewBag.CreditRatingFilter = new SelectList(ratings, CreditRatingFilter);


            var lastname = (from p in db.Client
                            select p.LastName)
                          .Distinct().OrderBy(p => p).ToList();
            ViewBag.LastNameFilter = new SelectList(lastname, LastNameFilter);

            ViewBag.CreditRatingFilter = new SelectList(ratings);

            return View();
        }

        public ActionResult Edit(int id)
        {
            var data = db.Client.Find(id);

            var items = new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            ViewBag.CreditRating = new SelectList(items);


            ViewData.Model = data;

            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            var Client = db.Client.Find(id);

            var ratings = (from p in db.Client
                           select p.CreditRating)
                        .Distinct().OrderBy(p => p).ToList();

            ViewBag.CreditRating = new SelectList(ratings, Client.CreditRating);


            if (TryUpdateModel(Client, includeProperties: new string[] { "ProductName" }))
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Client);
        }
    }
}