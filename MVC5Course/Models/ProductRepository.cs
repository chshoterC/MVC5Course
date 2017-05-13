using System;
using System.Linq;
using System.Collections.Generic;
using MVC5Course.Models.ViewModels;
using System.Data.Entity;

namespace MVC5Course.Models
{
    public class ProductRepository : EFRepository<Product>, IProductRepository
    {

        public override IQueryable<Product> All()
        {
            return base.All().Where(p => p.isDel == false);
        }

        public IQueryable<Product> All(bool showall)
        {
            if (showall)
                return base.All();
            else
                return this.All();
        }

        public Product Get單筆資料ById(int id)
        {
            return this.All().FirstOrDefault(p => p.ProductId == id);
        }

        public IQueryable<Product> GetProduct所有產品資料(bool Active, bool showAll = false)
        {
            IQueryable<Product> all = this.All();
            if (showAll)
                all = base.All();

            return all.Where(p => p.Active.HasValue && p.Active.Value == Active)
                .OrderByDescending(p => p.ProductId).Take(10);
        }

        public IQueryable<Product> GetProduct產品資料表List()
        {
            IQueryable<Product> all = base.All();

            return base.All().Take(50);


        }

        public void Update(Product product)
        {
            this.UnitOfWork.Context.Entry(product).State = EntityState.Modified;
        }

        public override void Delete(Product entity)
        {
            entity.isDel = true;
        }
    }

    public interface IProductRepository : IRepository<Product>
    {

    }
}