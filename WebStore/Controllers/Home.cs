using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController:Controller
    {
        private static readonly List<Employee> _Employees = new()
        {
            new Employee { Id = 1, LastName = "Иванов", FirstName = "Иван", MiddleName = "Иванович", Age = 30, EmploymentDate = "18.05.2020", Position = "Специалист отдела продаж" },
            new Employee { Id = 2, LastName = "Петров", FirstName = "Петр", MiddleName = "Петров", Age = 28, EmploymentDate = "10.05.2019", Position = "Менеджер по работе с клиентами" },
            new Employee { Id = 3, LastName = "Сергеев", FirstName = "Сергей", MiddleName = "Сергеевич", Age = 19, EmploymentDate = "18.01.2021", Position = "Специалист отдела поддержки" }
        };
        
        private readonly IConfiguration Configuration;
        public HomeController(IConfiguration Configuration) { this.Configuration = Configuration; }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SecondAction()
        {
            return Content(Configuration["Greetings"]);
        }

        public IActionResult Employees()
        {
            return View(_Employees);
        }

        public IActionResult Details(int id)
        {
            Employee employee = _Employees.Where(x => x.Id == id).FirstOrDefault();
            return View(employee);
        }

        public IActionResult Blog() => View();

        public IActionResult BlogSingle() => View();

        public IActionResult Cart() => View();

    }
}
