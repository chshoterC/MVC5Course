using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using MVC5Course.Models.ViewModels;

namespace MVC5Course.Controllers
{
    public class ClientsController : Controller
    {
        private FabricsEntities db = new FabricsEntities();

        // GET: Clients
        public ActionResult BatchUpdate()
        {
            GetClients();
            return View();
        }

        private void GetClients()
        {
            var Client = db.Client.Include(c => c.Occupation).Take(10);
            ViewData.Model = Client;
        }

        [HttpPost]
        public ActionResult BatchUpdate(ClientBatchUpdate[] items)
        {
            if(ModelState.IsValid)
            {
                foreach(var item in items)
                {
                    var cData = db.Client.Find(item.ClientId);
                    cData.FirstName = item.FirstName;
                    cData.MiddleName = item.MiddleName;
                    cData.LastName = item.LastName;
                }
                db.SaveChanges();

                return RedirectToAction("BatchUpdate");

            }

            GetClients();
            return View();
        }
    }
}
