﻿using Microsoft.AspNetCore.Mvc;
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
            new Employee { Id = 1, LastName = "Иванов", FirstName = "Иван", MiddleName = "Иванович", Age = 30 },
            new Employee { Id = 2, LastName = "Петров", FirstName = "Петр", MiddleName = "Петров", Age = 30 },
            new Employee { Id = 3, LastName = "Сергеев", FirstName = "Сергей", MiddleName = "Сергеевич", Age = 30 }
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
        
    }
}
