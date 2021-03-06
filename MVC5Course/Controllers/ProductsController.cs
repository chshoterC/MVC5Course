﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using MVC5Course.Models.ViewModels;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace MVC5Course.Controllers
{
    public class ProductsController : BaseController
    {

        private ProductRepository repo = RepositoryHelper.GetProductRepository();


        // GET: Products
        public ActionResult Index(bool Active = true)
        {
            var data = repo.GetProduct所有產品資料(Active);
            return View(data);
        }

        public ActionResult ListProducts(ListProductsQuery ListPDQuery)
        {
            var data = repo.GetProduct所有產品資料(true);

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(ListPDQuery.q))
                {
                    data = data.Where
                        (p => p.ProductName.Contains(ListPDQuery.q));
                }
                if (ListPDQuery.stock2 != 0)
                    data = data.Where(p => p.Stock > ListPDQuery.stock1 && p.Stock < ListPDQuery.stock2);
            }

            ViewData.Model = data
            .Select(p => new ListProducts()
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Price = p.Price,
                Stock = p.Stock
            })
                 .Take(50);
            return View();
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.Get單筆資料ById(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            //if (ModelState.IsValid)
            {
                repo.Add(product);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            //弱型別


            return View(product);
        }

        public class IEnumerableProduct
        {
            //Product product = db.Product.Find(id);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.Get單筆資料ById(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        public ActionResult Edit(int id, FormCollection form)
        {
            //if (ModelState.IsValid)
            var product = repo.Get單筆資料ById(id);
            if (TryUpdateModel(product, new string[] { "ProductId", "ProductName", "Price", "Active", "Stock" }))
            {
                repo.Update(product);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.Get單筆資料ById(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = repo.Get單筆資料ById(id);
            repo.UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;
            repo.Delete(product);
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(ListProducts datas)
        {
            if (ModelState.IsValid)
            {
                // TODO: 儲存資料進資料庫
                TempData["CreateProduct_Result"] = "新增成功";


                return RedirectToAction("ListProducts");
            }
            // 驗證失敗，繼續顯示原本的表單
            return View();
        }



    }
}
