using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain;
using WebStore.Domain.DTO;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.WebAPI.Controllers
{
    [Route(WebAPIAddress.Products)]
    [ApiController]
    public class ProductsApiController : ControllerBase
    {
        private readonly IProductData productData;

        public ProductsApiController(IProductData ProductData)=> productData = ProductData;

        [HttpGet("sections")]
        public IActionResult GetSections() => Ok(productData.GetSections().ToDTO());

        [HttpGet("sections/{id:int}")]
        public IActionResult GetSection(int id) => Ok(productData.GetSection(id).ToDTO());

        [HttpGet("brands")]
        public IActionResult GetBrands() => Ok(productData.GetBrands().ToDTO());

        [HttpGet("brand/{id:int}")]
        public IActionResult GetBrand(int id) => Ok(productData.GetBrand(id).ToDTO());

        [HttpPost]
        public IActionResult GetProducts(ProductFilter Filter = null) => Ok(productData.GetProducts(Filter).ToDTO());

        [HttpGet("{id:int}")]
        public IActionResult GetProduct(int id) => Ok(productData.GetProductById(id).ToDTO());
    }
}
