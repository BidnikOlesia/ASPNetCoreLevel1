using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Interfaces.Services.TestAPI;

namespace WebStore.Controllers
{
    public class WebAPIController : Controller
    {
        private readonly IValuesService valuesService;

        public WebAPIController(IValuesService ValuesService)=> valuesService = ValuesService;

        public IActionResult Index()
        {
            var values = valuesService.GetAll();
            return View(values);
        }
    }
}
