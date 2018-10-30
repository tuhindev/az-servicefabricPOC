using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using MyEcommerceApp.ProductCatalogue.Models;
namespace MyEcommerceApp.ProductCatalogue
{
    public class ProductCatalogueRepository : IProductCatalogueRepository
    {
        IReliableStateManager _dataManager;
        string _productCollectionName;
        public ProductCatalogueRepository(IReliableStateManager dataManager)
        {
            this._productCollectionName = "products";
            this._dataManager = dataManager;
        }
        public async Task AddProduct(Product product)
        {
            var products = await this._dataManager.GetOrAddAsync<IReliableDictionary<Guid, Product>>(this._productCollectionName);
            using (var transaction = this._dataManager.CreateTransaction())
            {
                await products.AddOrUpdateAsync(transaction, product.ProductId, product, (i, v) => product);
                await transaction.CommitAsync();
            }
        }

        public async Task DeleteProduct(Guid productId)
        {
            var products = await this._dataManager.GetOrAddAsync<IReliableDictionary<Guid, Product>>(this._productCollectionName);
            using (var transaction = this._dataManager.CreateTransaction())
            {
                await products.TryRemoveAsync(transaction, productId);
                await transaction.CommitAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var products = await this._dataManager.GetOrAddAsync<IReliableDictionary<Guid, Product>>(this._productCollectionName);
            var outProducts = new List<Product>();
            using (var transaction = this._dataManager.CreateTransaction())
            {
                var allProducts = await products.CreateEnumerableAsync(transaction, EnumerationMode.Unordered);

                using (var enumerator = allProducts.GetAsyncEnumerator())
                {
                    while (await enumerator.MoveNextAsync(CancellationToken.None))
                    {
                        var current = enumerator.Current;
                        outProducts.Add(current.Value);
                    }
                }
            }
            return outProducts;
        }

        public Task<Product> UpdateProduct(Product product, Guid productId)
        {
            throw new NotImplementedException();
        }
    }
}
