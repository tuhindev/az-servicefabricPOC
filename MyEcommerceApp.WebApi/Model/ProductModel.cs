using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEcommerceApp.WebApi.Model
{
    public class ProductModel
    {
        [JsonProperty("id")]
        public Guid ProductId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
