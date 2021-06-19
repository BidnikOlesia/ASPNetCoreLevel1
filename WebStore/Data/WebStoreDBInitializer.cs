using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Data
{
    public class WebStoreDBInitializer
    {
        private readonly WebStoreDB _db;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly ILogger<WebStoreDBInitializer> _logger;

        public WebStoreDBInitializer(
            WebStoreDB db, 
            UserManager<User> UserManager,
            RoleManager<Role> RoleManager,
            ILogger<WebStoreDBInitializer> Logger)
        {
            _db = db;
            userManager = UserManager;
            roleManager = RoleManager;
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

            try
            {
                InitializeIdentityAsync().GetAwaiter().GetResult();
            }
            catch (Exception e)
            {

                _logger.LogError(e, "Ошибка при инициализации данных БД системы Identity");
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

        private async Task InitializeIdentityAsync()
        {
            _logger.LogInformation("Инициализация БД системы Identity");
            var timer = Stopwatch.StartNew();

            async Task CheckRole(string RoleName)
            {
                if (!await roleManager.RoleExistsAsync(RoleName))
                {
                    _logger.LogInformation($"Роль {RoleName} отсутствует. Создаем");
                    await roleManager.CreateAsync(new Role { Name = RoleName });
                    _logger.LogInformation($"Роль {RoleName} создана успешно");
                }
                    
            }

            await CheckRole(Role.Administrators);
            await CheckRole(Role.Users);

            if(await userManager.FindByNameAsync(User.Administrator) is null)
            {
                _logger.LogInformation($"Пользователь {User.Administrator} отстуствует. Создаем");

                var admin = new User
                {
                    UserName = User.Administrator
                };

                var creation_result = await userManager.CreateAsync(admin, User.DefaultAdminPassword);
                if (creation_result.Succeeded)
                {
                    _logger.LogInformation($"Пользователь {User.Administrator} успешно создан");
                    await userManager.AddToRoleAsync(admin, Role.Administrators);
                    _logger.LogInformation($"Пользователь {User.Administrator} наделен ролью {Role.Administrators}");

                }
                else
                {
                    var errors = creation_result.Errors.Select(e => e.Description).ToArray();
                    _logger.LogError($"Учетная запись администратора несоздана по причине {string.Join(", ", errors)}");
                    throw new InvalidOperationException($"Ошибка при создании пользователя {User.Administrator}: {string.Join(", ", errors)}");
                }
            }

            _logger.LogInformation($"Инициализация БД выполнена за {timer.Elapsed.TotalSeconds}");
        }
    }
}
