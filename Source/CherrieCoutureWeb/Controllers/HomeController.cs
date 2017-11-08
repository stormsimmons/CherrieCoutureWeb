using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CherrieCoutureWeb.Models;
using Microsoft.AspNetCore.Authorization;

namespace CherrieCoutureWeb.Controllers
{
	[Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
			
            ViewData["Message"] = "Your application description page.";

			
			return View();
        }

        public IActionResult Contact()
        {

			var username = User.Identity.Name;

            ViewData["Message"] = $"Your contact page. {username}";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
