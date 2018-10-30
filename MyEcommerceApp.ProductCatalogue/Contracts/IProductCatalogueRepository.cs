using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyEcommerceApp.ProductCatalogue.Models;
namespace MyEcommerceApp.ProductCatalogue
{
    interface IProductCatalogueRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task AddProduct(Product product);
        Task DeleteProduct(Guid productId);
        Task<Product> UpdateProduct(Product product, Guid productId);
    }
}
