using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CherrieCoutureWeb.Models.Product
{
    public class Product
    {
		public string Id { get; set; }
		public int ProductId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }
		public decimal Price { get; set; }
		public string Size { get; set; }
		public string Category { get; set; }
		public int StockQuantity { get; set; }
	}
}
