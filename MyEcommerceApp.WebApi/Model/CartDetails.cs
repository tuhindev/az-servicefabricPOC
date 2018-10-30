using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEcommerceApp.WebApi.Model
{
    public class CartDetails
    {
        public string UserId { get; set; }
        public IEnumerable<CartItem> Items { get; set; }
    }
}
