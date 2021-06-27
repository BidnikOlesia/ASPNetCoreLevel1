﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewsModels;

namespace WebStore.Infrastructure.Mapping
{
    public static class ProductMapper
    {
        public static ProductViewModel ToView(this Product product) => product is null ?
            null
            : new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Section = product.Section.Name,
                Brand = product.Brand?.Name
            };

        public static IEnumerable<ProductViewModel> ToView(this IEnumerable<Product> products) => products.Select(ToView);
    }
}
