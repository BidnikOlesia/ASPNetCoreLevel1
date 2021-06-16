using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;

namespace WebStore.Data
{
    public class WebStoreDBInitializer
    {
        private readonly WebStoreDB _db;
        private readonly ILogger<WebStoreDBInitializer> _logger;

        public WebStoreDBInitializer(WebStoreDB db, ILogger<WebStoreDBInitializer> Logger)
        {
            _db = db;
            _logger = Logger;
        }

        public void Initialize()
        {
            _logger.LogInformation("Инициализация БД");
            var timer = Stopwatch.StartNew();

            if (_db.Database.GetPendingMigrations().Any())
            {
                _logger.LogInformation("Миграция БД");
                _db.Database.Migrate();
                _logger.LogInformation($"Миграция БД завершена за {timer.Elapsed.TotalSeconds}c");
            }
            else
                _logger.LogInformation($"Миграция БД не требуется. {timer.Elapsed.TotalSeconds}c");

            try
            {
                InitializeProducts();
            }
            catch (Exception e)
            {

                _logger.LogError(e, "Ошибка при инициализации товаров в БД");
                throw;
            }
            _logger.LogInformation($"Инициализация БД завершена за {timer.Elapsed.TotalSeconds}c");
        }

        private void InitializeProducts()
        {
            if (_db.Products.Any())
            {
                _logger.LogInformation("БД не нуждается в инициализации товаров");
                return;
            }

            var timer = Stopwatch.StartNew();

            var sections_pool = TestData.Sections.ToDictionary(secction => secction.Id);
            var brands_pool = TestData.Brands.ToDictionary(brand => brand.Id);

            foreach (var section in TestData.Sections.Where(s => s.ParentId != null))
                section.Parent = sections_pool[(int)section.ParentId];

            foreach(var product in TestData.Products)
            {
                product.Section = sections_pool[product.SectionId];
                if (product.BrandId is { } brand_id)
                    product.Brand = brands_pool[brand_id];

                product.Id = 0;
                product.SectionId = 0;
                product.BrandId = null;
            }

            foreach(var section in TestData.Sections)
            {
                section.Id = 0;
                section.ParentId = null;
            }

            foreach (var brand in TestData.Brands)
                brand.Id = 0;

            using (_db.Database.BeginTransaction())
            {
                _db.Sections.AddRange(TestData.Sections);
                _db.Brands.AddRange(TestData.Brands);
                _db.Products.AddRange(TestData.Products);
                _db.SaveChanges();
                _db.Database.CommitTransaction();
            }
            _logger.LogInformation($"Инициализация товаров выполнена за {timer.Elapsed.TotalSeconds}c");
        }
    }
}
