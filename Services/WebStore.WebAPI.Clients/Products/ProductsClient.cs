using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain;
using WebStore.Domain.DTO;
using WebStore.Domain.Entities;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;
using WebStore.WebAPI.Clients.Base;

namespace WebStore.WebAPI.Clients.Products
{
    public class ProductsClient : BaseClient, IProductData
    {
        public ProductsClient(HttpClient Client) : base(Client, WebAPIAddress.Products) { }

        public Brand GetBrand(int id) => Get<BrandDTO>($"{Address}/brands/{id}").FromDTO();

        public IEnumerable<Brand> GetBrands() => Get<IEnumerable<BrandDTO>>($"{Address}/brands").FromDTO();

        public Product GetProductById(int Id) => Get<ProductDTO>($"{Address}/{Id}").FromDTO();

        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
            var response = Post(Address, Filter);
            var products = response.Content.ReadFromJsonAsync<IEnumerable<ProductDTO>>().Result;
            return products.FromDTO();
        }

        public Section GetSection(int id) => Get<SectionDTO>($"{Address}/sections/{id}").FromDTO();

        public IEnumerable<Section> GetSections() => Get<IEnumerable<SectionDTO>>($"{Address}/sections").FromDTO();
    }
}
