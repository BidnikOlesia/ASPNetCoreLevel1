using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

            if (_db.Database.GetPendingMigrations().Any())
            {
                _logger.LogInformation("Миграция БД");
                _db.Database.Migrate();
                _logger.LogInformation("Миграция БД завершена");
            }
            else
                _logger.LogInformation("Миграция БД не требуется");

            try
            {
                InitializeProducts();
            }
            catch (Exception e)
            {

                _logger.LogError(e, "Ошибка при инициализации товаров в БД");
                throw;
            }
            _logger.LogInformation("Инициализация БД завершена");
        }

        private void InitializeProducts()
        {
            if (_db.Products.Any())
            {
                _logger.LogInformation("БД не нуждается в инициализации товаров");
                return;
            }

            _logger.LogInformation("Инициализация секций");
            using (_db.Database.BeginTransaction())
            {
                _db.Sections.AddRange(TestData.Sections);
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] ON");
                _db.SaveChanges();
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] OFF");
                _db.Database.CommitTransaction();
            }
            _logger.LogInformation("Инициализация секций выполнена");

            _logger.LogInformation("Инициализация брендов");
            using (_db.Database.BeginTransaction())
            {
                _db.Brands.AddRange(TestData.Brands);
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] ON");
                _db.SaveChanges();
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] OFF");
                _db.Database.CommitTransaction();
            }
            _logger.LogInformation("Инициализация брендов выполнена");

            _logger.LogInformation("Инициализация товаров");
            using (_db.Database.BeginTransaction())
            {
                _db.Products.AddRange(TestData.Products);
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] ON");
                _db.SaveChanges();
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] OFF");
                _db.Database.CommitTransaction();
            }
            _logger.LogInformation("Инициализация товаров выполнена");
        }
    }
}
