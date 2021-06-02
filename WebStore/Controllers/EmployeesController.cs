using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;
using WebStore.Services.Interfaces;

namespace WebStore.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _EmployeesData;

        public EmployeesController(IEmployeesData EmployeesData)
        {
            _EmployeesData = EmployeesData;
        }

        public IActionResult Index()=> View(_EmployeesData.GetAll());

        public IActionResult EmployeeDetails(int id)
        {
            Employee employee = _EmployeesData.Get(id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }
    }
}
