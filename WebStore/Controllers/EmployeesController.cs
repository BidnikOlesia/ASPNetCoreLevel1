using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult Index()=> View(_Employees);

        public IActionResult EmployeeDetails(int id)
        {
            Employee employee = _Employees.Where(x => x.Id == id).FirstOrDefault();
            if (employee == null)
                return NotFound();
            return View(employee);
        }
    }
}
