using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.ViewsModels;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        public IActionResult Index()=> View(new CartOrderViewModel { Cart = cartService.GetViewModel()});

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

        [Authorize]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOut (OrderViewModel OrderModel, [FromServices] IOrderService OrderService)
        {
            if (!ModelState.IsValid)
                return View(nameof(Index), new CartOrderViewModel 
                {
                    Cart = cartService.GetViewModel(),
                    Order = OrderModel
                });

            var order = await OrderService.CreateOrder(
                User.Identity.Name,
                cartService.GetViewModel(),
                OrderModel);

            cartService.Clear();

            return RedirectToAction(nameof(OrderConfirmed), new { order.Id});
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }

    }
}
