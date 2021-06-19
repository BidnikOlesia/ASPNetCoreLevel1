using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Services.Interfaces;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        public IActionResult Index()=> View(cartService.GetViewModel());

        public IActionResult Add(int id)
        {
            cartService.Add(id);
            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Remove(int id)
        {
            cartService.Remove(id);
            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Decrement(int id)
        {
            cartService.Decrement(id);
            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Clear()
        {
            cartService.Clear();
            return RedirectToAction("Index", "Cart");
        }
    }
}
