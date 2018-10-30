using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyEcommerceApp.WebApi.Model;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using MyEcommerceApp.ProductCatalogue.Models;
using Microsoft.ServiceFabric.Services.Client;
using GenFu;
namespace MyEcommerceApp.WebApi.Controllers
{
    //TODO:
    /*
     * 1. Add circuit breaker/retry mechanism
     * 2. Add mvc filters to leverage mvc features [e.g. error filter, result filters etc.]
         */
    /// <summary>
    /// Product controller to demonstrate the usage of Servicefaric reliable service.
    /// </summary>
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private IProductCatalogueService _productCatalogService;

        public ProductController()
        {
            //TODO: Apply partitioning of data, since this is currently supporting only 1 partition.
            this._productCatalogService = ServiceProxy.Create<IProductCatalogueService>(new Uri("fabric:/MyEcommerceApp/MyEcommerceApp.ProductCatalogue"), new ServicePartitionKey(0));
        }
        [Route("getall")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var exception = new Exception();
            try
            {
                var products = await this._productCatalogService.GetAllProducts();
                if (products == null)
                {
                    return Ok();
                }
                return Ok(products.Select(m => new ProductModel { ProductId = m.ProductId, Description = m.Description, Name = m.Name, Price = m.Price }));
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            //TODO: send the details error message when request is made from dev, stage env only. 
            //1. add AI
            //2. add custom exception filter
            return BadRequest(exception);
        }

        [Route("seed")]
        [HttpGet]
        public async Task<IActionResult> SeedData()
        {
            var exception = new Exception();
            try
            {
                await this._productCatalogService.AddProduct(A.New<Product>());
                return Accepted();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return BadRequest(exception);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            //async hack
            await Task.Delay(1);
            return BadRequest(new NotImplementedException());
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ProductModel value)
        {
            await Task.Delay(1);
            return BadRequest(new NotImplementedException());
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]string value)
        {
            await Task.Delay(1);
            return BadRequest(new NotImplementedException());
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Task.Delay(1);
            return BadRequest(new NotImplementedException());
        }
    }
}
