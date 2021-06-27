using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Identity;
using WebStore.Services.Interfaces;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = Role.Administrators)]
    public class ProductsController : Controller
    {
        private readonly IProductData productData;

        public ProductsController(IProductData ProductData)=> productData = ProductData;

        public IActionResult Index()=> View(productData.GetProducts());

        public IActionResult Edit(int id) =>
            productData.GetProductById(id) is { } product
            ? View(product)
            : NotFound();

        public IActionResult Delete(int id) =>
            productData.GetProductById(id) is { } product
            ? View(product)
            : NotFound();

    }
}
