using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using MyEcommerceApp.UserActors.Interfaces;
using MyEcommerceApp.WebApi.Model;
namespace MyEcommerceApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        [Route("cartbyuser/userid={userid}")]
        [HttpGet]
        public async Task<IActionResult> Get(string userId)
        {
            var exception = new Exception();
            try
            {
                IUserActors actor = GetActor(userId);

                Dictionary<Guid, int> products = await actor.GetCart();

                return Ok(new CartDetails()
                {
                    UserId = userId,
                    Items = products.Select(
                        p => new CartItem { ProductId = p.Key.ToString(), Quantity = p.Value }).ToArray()
                });
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return BadRequest(exception);
        }

        [Route("addtocart/userid={userid}")]
        [HttpPost]
        public async Task<IActionResult> Add(string userId, [FromBody] AddCartItem request)
        {
            var exception = new Exception();
            try
            {
                IUserActors actor = GetActor(userId);
                await actor.AddToCart(request.ProductId, request.Quantity);
                return Accepted();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return BadRequest(exception);
        }


        private IUserActors GetActor(string userId)
        {
            return ActorProxy.Create<IUserActors>(new ActorId(userId), new Uri("fabric:/MyEcommerceApp/UserActorsActorService"));
        }
    }
}