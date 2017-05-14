using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [SharedViewBag]
        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";

            throw new Exception();

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        [SharedViewBag(MyProperty = "")]
        public ActionResult PartialAbout()
        {
            //ViewBag.Message = "<table><tr>ddd</tr></table>";
            if (Request.IsAjaxRequest())
            {
                return PartialView("About");
            }
            else
            {
                return View("About");
            }
        }

        public ActionResult SomeAction()
        {
            return PartialView("SuccessRedirect", "/");
        }

        public ActionResult GetFile()
        {
            return File(Server.MapPath("~/Content/fbshare.jpg"), "image/jpeg", "new.jpg");
        }

        public ActionResult GetJson()
        {
            db.Configuration.LazyLoadingEnabled = false;

            return Json(db.Product.Take(5), JsonRequestBehavior.AllowGet);
        }

        public ActionResult VT()
        {
            ViewBag.IsEnable = true;

            return View();
        }

        public ActionResult RazorTest()
        {
            ViewData.Model  = new int[] { 1, 2, 3, 4, 5 };
            return PartialView();
        }
    }
}