using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Services.Services.InSQL
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreDB _db;

        public SqlProductData(WebStoreDB db) => _db = db;

        public IEnumerable<Brand> GetBrands() => _db.Brands.Include(s => s.Products);

        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
            IQueryable<Product> query = _db.Products.Include(p => p.Brand).Include(p => p.Section);

            if (Filter?.Ids?.Length > 0)
            {
                query = query.Where(product => Filter.Ids.Contains(product.Id));
            }
            else
            {
                if (Filter?.SectionId is { } section_id)
                    query = query.Where(product => product.SectionId == section_id);

                if (Filter?.BrandId is { } brand_id)
                    query = query.Where(product => product.BrandId == brand_id);
            }



            return query;
        }

        public IEnumerable<Section> GetSections() => _db.Sections.Include(s=>s.Products);

        public Product GetProductById(int Id) => _db.Products.Include(p => p.Brand).Include(p => p.Section).SingleOrDefault(p => p.Id == Id);

        public Section GetSection(int id) => _db.Sections.Include(s => s.Products).FirstOrDefault(s => s.Id == id);

        public Brand GetBrand(int id) => _db.Brands.Include(s => s.Products).FirstOrDefault(s => s.Id == id);
    }
}
