using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CherrieCoutureWeb.Models.Product
{
    public class CreateProductViewModel
    {
		public Product Product { get; set; }
		public IFormFileCollection Files { get; set; }
	}
}
