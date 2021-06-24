﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Services.Interfaces;
using WebStore.ViewsModels;

namespace WebStore.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        public IActionResult Index()=> View();

        public async Task<IActionResult> Orders([FromServices]IOrderService OrderService)
        {
            var orders = await OrderService.GetUserOrder(User.Identity!.Name);

            return View(orders.Select(o=>new UserOrderViewModel 
            {
                Id = o.Id,
                Name = o.Name,
                Phone = o.Phone,
                Address = o.Address,
                TotalPrice = o.Items.Sum(item=>item.TotalItemPrice)
            }));
        }
    }
}
