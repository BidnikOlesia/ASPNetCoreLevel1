using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebStore.DAL.Context
{
    public class WebStoreDB:IdentityDbContext<User,Role, string>
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public WebStoreDB(DbContextOptions<WebStoreDB> options) : base(options){ }
    }
}
