using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using MyEcommerceApp.ProductCatalogue.Models;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using GenFu;
using GenFu.ValueGenerators;
namespace MyEcommerceApp.ProductCatalogue
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class ProductCatalogue : StatefulService, IProductCatalogueService
    {
        private IProductCatalogueRepository _productRepository;
        public ProductCatalogue(StatefulServiceContext context)
            : base(context)
        { }

        public async Task AddProduct(Product product)
        {
            await this._productRepository.AddProduct(product);
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await this._productRepository.GetAllProducts();
        }

        /// <summary>
        /// Optional override to create listeners (e.g., HTTP, Service Remoting, WCF, etc.) for this service replica to handle client or user requests.
        /// </summary>
        /// <remarks>
        /// For more information on service communication, see https://aka.ms/servicefabricservicecommunication
        /// </remarks>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return this.CreateServiceRemotingReplicaListeners();
        }

        /// <summary>
        /// This is the main entry point for your service replica.
        /// This method executes when this replica of your service becomes primary and has write status.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service replica.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            try
            {
                await Task.Run(() => { this._productRepository = new ProductCatalogueRepository(this.StateManager); });
            }
            catch (Exception ex)
            {
                //TODO: LOG exception with either: the SF helper class or AI
                
            }
            //var product1 = A.New<Product>();
            //await this._productRepository.AddProduct(product1);
        }
    }
}
