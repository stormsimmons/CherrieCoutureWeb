using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CherrieCoutureWeb.APIGateway;
using CherrieCoutureWeb.Models.Product;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace CherrieCoutureWeb.Controllers
{
	[Authorize]
    public class ProductController : Controller
    {
		private ApiGateway _apiGateway;
		public ProductController()
		{
			_apiGateway = new ApiGateway();
		}

		public IActionResult Index()
        {
			var productList = _apiGateway.GetAsJsonAsync<List<Product>>("product/list").Result;

            return View(productList);
        }
		[Authorize(Roles ="Admin")]
		public IActionResult Create()
		{
			return View();
		}

		[Authorize(Roles ="Admin")]
		[HttpPost]
		public IActionResult Create(Product product)
		{
			var image = Request.Form.Files[0];

			var target = Path.GetFullPath("../CherrieCoutureWeb/wwwroot/images/products/");
			var filePath = Path.Combine(target, image.FileName);
			image.CopyTo(new FileStream(filePath, FileMode.Create));

			product.ImageUrl = image.FileName;

			var response = _apiGateway.PostAsJsonAsync<object>("product/add", product).Result;

			return RedirectToAction("Index", "Product");
			
		}

		[Authorize(Roles = "Admin")]
		[HttpGet]
		public IActionResult Delete(string id)
		{
			var product = _apiGateway.GetAsJsonAsync<Product>($"product/getproduct?Id={id}").Result;
			return View(product);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Delete(Product product)
		{
			var response = _apiGateway.PostAsJsonAsync<object>("product/delete" ,new { Id = product.Id }).Result;

			return RedirectToAction("Index", "Product");
		}

		[Authorize(Roles = "Admin")]
		[HttpGet]
		public IActionResult Update(string id)
		{
			var product = _apiGateway.GetAsJsonAsync<Product>($"product/getproduct?Id={id}").Result;
			return View(product);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Update(Product product)
		{
			var response = _apiGateway.PostAsJsonAsync<object>("product/update", product).Result;

			return RedirectToAction("Index", "Product");
		}


	}
}