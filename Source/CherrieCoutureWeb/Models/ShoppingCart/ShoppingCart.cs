using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CherrieCoutureWeb.Models.Product;

namespace CherrieCoutureWeb.Models.ShoppingCart
{
    public class ShoppingCart
    {
		public string Id { get; set; }
		public string UserId { get; set; }
		public string UserName { get; set; }
		public IList<Product.Product> ProductList { get; set; }
	}
}
