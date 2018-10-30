using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEcommerceApp.WebApi.Model
{
    public class AddCartItem
    {
        [JsonProperty("productId")]
        public Guid ProductId { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}
