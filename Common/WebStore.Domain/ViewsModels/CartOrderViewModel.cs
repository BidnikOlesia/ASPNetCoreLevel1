using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewsModels
{
    public class CartOrderViewModel
    {
        public CartViewModel Cart { get; set; }

        public OrderViewModel Order { get; set; } = new();
    }
}
