using MyEcommerceApp.ProductCatalogue.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace MyEcommerceApp.ProductCatalogue.Models
{
    
    public interface IProductCatalogueService : IService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task AddProduct(Product product);
    }
}

