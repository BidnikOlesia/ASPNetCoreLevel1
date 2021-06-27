using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Mapping;
using WebStore.Services.Interfaces;
using WebStore.ViewsModels;

namespace WebStore.Services.InCookies
{
    public class InCookiesCartService : ICartService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IProductData productData;
        private readonly string cartName;

        public Cart Cart 
        {
            get
            {
                var context = httpContextAccessor.HttpContext;
                var cookies = context!.Response.Cookies;

                var cart_cookie = context.Request.Cookies[cartName];
                if(cart_cookie is null)
                {
                    var cart = new Cart();
                    cookies.Append(cartName, JsonConvert.SerializeObject(cart));
                    return cart;
                }

                ReplaceCookies(cookies, cart_cookie);
                return JsonConvert.DeserializeObject<Cart>(cart_cookie);
            }
            set => ReplaceCookies(httpContextAccessor.HttpContext.Response.Cookies, JsonConvert.SerializeObject(value));
        }

        private void ReplaceCookies(IResponseCookies cookies, string cookie)
        {
            cookies.Delete(cartName);
            cookies.Append(cartName, cookie);
        }
        public InCookiesCartService(IHttpContextAccessor HttpContextAccessor, IProductData ProductData)
        {
            httpContextAccessor = HttpContextAccessor;
            productData = ProductData;

            var user = httpContextAccessor.HttpContext!.User;
            var user_name = user.Identity!.IsAuthenticated ? $"-{user.Identity.Name}" : null;

            cartName = $"WebStore.Cart{user_name}";
        }


        public void Add(int Id)
        {
            var cart = Cart;

            var item = Cart.Items.FirstOrDefault(item => item.ProductId == Id);
            if (item is null)
                cart.Items.Add(new CartItem { ProductId = Id });
            else
                item.Quantity++;

            Cart = cart;
        }

        public void Clear()
        {
            var cart = Cart;
            cart.Items.Clear();
            Cart = cart;
        }

        public void Decrement(int Id)
        {
            var cart = Cart;

            var item = Cart.Items.FirstOrDefault(item => item.ProductId == Id);
            if (item is null) return;

            if (item.Quantity > 0)
                item.Quantity--;

            if (item.Quantity <= 0)
                cart.Items.Remove(item);

            Cart = cart; ;
        }

        public CartViewModel GetViewModel()
        {
            var products = productData.GetProducts(new ProductFilter
            {
                Ids = Cart.Items.Select(i => i.ProductId).ToArray()
            });

            var products_views = products.ToView().ToDictionary(p => p.Id);

            return new CartViewModel
            {
                Items = Cart.Items.Where(i => products_views.ContainsKey(i.ProductId)).Select(i => (products_views[i.ProductId], i.Quantity))
            };
        }

        public void Remove(int Id)
        {
            var cart = Cart;

            var item = Cart.Items.FirstOrDefault(item => item.ProductId == Id);
            if (item is null) return;

            cart.Items.Remove(item);

            Cart = cart;
        }
    }
}
