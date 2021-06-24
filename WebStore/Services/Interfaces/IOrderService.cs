using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Orders;
using WebStore.ViewsModels;

namespace WebStore.Services.Interfaces
{
    interface IOrderService
    {
        Task<IEnumerable<Order>> GetUserOrder(string UserName);

        Task<Order> GetOrderById(int id);

        Task<Order> CreateOrder(string UserName, CartViewModel Cart, OrderViewModel OrderModel);
    }
}
