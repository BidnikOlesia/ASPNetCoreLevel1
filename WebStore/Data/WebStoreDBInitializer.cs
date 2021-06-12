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
            _logger.LogInformation("Инициализация секций");
            using (_db.Database.BeginTransaction())
            {
                _db.Sections.AddRange(TestData.Sections);
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] ON");
                _db.SaveChanges();
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] OFF");
                _db.Database.CommitTransaction();
            }
            _logger.LogInformation($"Инициализация секций выполнена за {timer.Elapsed.TotalSeconds}c");

            _logger.LogInformation("Инициализация брендов");
            using (_db.Database.BeginTransaction())
            {
                _db.Brands.AddRange(TestData.Brands);
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] ON");
                _db.SaveChanges();
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] OFF");
                _db.Database.CommitTransaction();
            }
            _logger.LogInformation($"Инициализация брендов выполнена за {timer.Elapsed.TotalSeconds}c");

            _logger.LogInformation("Инициализация товаров");
            using (_db.Database.BeginTransaction())
            {
                _db.Products.AddRange(TestData.Products);
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] ON");
                _db.SaveChanges();
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] OFF");
                _db.Database.CommitTransaction();
            }
            _logger.LogInformation($"Инициализация товаров выполнена за {timer.Elapsed.TotalSeconds}c");
        }
    }
}
