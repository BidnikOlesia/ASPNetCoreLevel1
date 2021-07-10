using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewsModels;
using Assert = Xunit.Assert;

namespace WebStore.Services.Tests.Services
{
    [TestClass]
    public class CartServiceTests
    {
        private Cart _Cart;

        [TestInitialize]
        public void Initialize()
        {
            _Cart = new Cart
            {
                Items = new List<CartItem>
                {
                    new(){ProductId = 1, Quantity  = 1},
                    new(){ProductId = 1, Quantity  = 3},
                }
            };
        }
        
        [TestMethod]
        public void Cart_Class_ItemsCount_returns_Correcr_Quantity()
        {
            var cart = _Cart;
            var expected_items_count = _Cart.Items.Sum(i => i.Quantity);

            var actual_items_count = cart.ItemsCount;

            Assert.Equal(expected_items_count, actual_items_count);
        }

        [TestMethod]
        public void CartViewModel_returns_correct_ItemsCount()
        {
            var cart_view_model = new CartViewModel
            {
                Items = new[]
                {
                    ( new ProductViewModel { Id = 1, Name = "Product 1", Price = 0.5m }, 1 ),
                    ( new ProductViewModel { Id = 2, Name = "Product 2", Price = 1.5m }, 3 ),
                }
            };

            const int expected_count = 4;

            var actual_count = cart_view_model.ItemsCount;

            Assert.Equal(expected_count, actual_count);
        }
    }
}
