using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CherrieCoutureWeb.APIGateway;
using CherrieCoutureWeb.Models.User;
using CherrieCoutureWeb.Models.Product;
using CherrieCoutureWeb.Models.ShoppingCart;

namespace CherrieCoutureWeb.Controllers
{
    public class ShoppingCartController : Controller
    {
		private ApiGateway _apiGateway;
		public ShoppingCartController()
		{
			_apiGateway = new ApiGateway();
		}
        public IActionResult Index()
        {
			var username = Request.Cookies["username"];
			var shoppingCart = _apiGateway.GetAsJsonAsync<ShoppingCart>($"shoppingcart/getcart?UserName={username}").Result;
            return View(shoppingCart.ProductList);
        }


    }
}