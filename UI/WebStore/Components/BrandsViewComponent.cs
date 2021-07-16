using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.ViewsModels;
using WebStore.Interfaces.Services;

namespace WebStore.Components
{
    public class BrandsViewComponent:ViewComponent
    {
        private readonly IProductData _ProductData;

        public BrandsViewComponent(IProductData ProductData) => _ProductData = ProductData;

        public IViewComponentResult Invoke(string BrandId) => View(GetBrands());

        public IEnumerable<BrandViewModel> GetBrands() => _ProductData.GetBrands().OrderBy(x => x.Order).Select(x => new BrandViewModel
        {
            Id = x.Id,
            Name = x.Name
        });
    }
}
