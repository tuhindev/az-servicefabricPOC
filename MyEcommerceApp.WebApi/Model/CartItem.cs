using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEcommerceApp.WebApi.Model
{
    public class CartItem
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public int  Quantity { get; set; }
    }
}
