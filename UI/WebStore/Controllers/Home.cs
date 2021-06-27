using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Controllers
{
    public class HomeController:Controller
    {
        
        private readonly IConfiguration Configuration;
        public HomeController(IConfiguration Configuration) { this.Configuration = Configuration; }
        public IActionResult Index([FromServices]IProductData ProductData)
        {
            var products = ProductData.GetProducts().Take(9).ToView();

            ViewBag.Products = products;
            //ViewData["Products"] = products;

            return View();
        }

        public IActionResult SecondAction()
        {
            return Content(Configuration["Greetings"]);
        }

        public IActionResult Blog() => View();

        public IActionResult BlogSingle() => View();

        public IActionResult Cart() => View();

        public IActionResult Checkout() => View();

        public IActionResult ContactUs() => View();

        public IActionResult Login() => View();

        public IActionResult ProductDetails() => View();

        public IActionResult Shop() => View();

        public IActionResult Page404() => View();

    }
}
