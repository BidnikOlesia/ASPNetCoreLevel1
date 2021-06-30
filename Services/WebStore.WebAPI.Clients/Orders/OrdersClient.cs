using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.DTO;
using WebStore.Domain.Entities.Orders;
using WebStore.Domain.ViewsModels;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;
using WebStore.WebAPI.Clients.Base;

namespace WebStore.WebAPI.Clients.Orders
{
    public class OrdersClient : BaseClient, IOrderService
    {
        public OrdersClient(HttpClient Client) : base(Client, WebAPIAddress.Orders) { }


        public async Task<IEnumerable<Order>> GetUserOrder(string UserName)
        {
            var orders_dto = await GetAsync<IEnumerable<OrderDTO>>($"{Address}/user/{UserName}").ConfigureAwait(false);
            return orders_dto.FromDTO();
        }

        public async Task<Order> GetOrderById(int id)
        {
            var order_dto = await GetAsync<OrderDTO>($"{Address}/{id}").ConfigureAwait(false);
            return order_dto.FromDTO();
        }

        public async Task<Order> CreateOrder(string UserName, CartViewModel Cart, OrderViewModel OrderModel)
        {
            var create_order_model = new CreateOrderDTO
            {
                Items = Cart.ToDTO(),
                Order = OrderModel,
            };
            var response = await PostAsync($"{Address}/{UserName}", create_order_model).ConfigureAwait(false);
            var order_dto = await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<OrderDTO>().ConfigureAwait(false);
            return order_dto.FromDTO();
        }
    }
}
