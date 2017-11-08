﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CherrieCoutureWeb.Models.Login;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using CherrieCoutureWeb.Models.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using CherrieCoutureWeb.APIGateway;
using Newtonsoft.Json;
using CherrieCoutureWeb.APIGateway.ResponseModels;
using System.Web;
using System.Net.Http;

namespace CherrieCoutureWeb.Controllers
{

	public class AccountsController : Controller
	{
		private ApiGateway _apiGateway;
		public AccountsController()
		{
			_apiGateway = new ApiGateway();
		}
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login([FromForm]LoginViewModel model, string returnUrl = null)
		{
			if (model != null)
			{
				var response = _apiGateway.PostAsJsonAsync<LoginValidationResponse>("login/validate", model).Result;

				if (response.ValidCredentials == true)
				{
					var claims = new List<Claim>
						{
							new Claim(ClaimTypes.Name, response.LoggedOnUser.FirstName),
							new Claim(ClaimTypes.Surname , response.LoggedOnUser.LastName),
							new Claim(ClaimTypes.Email , response.LoggedOnUser.Email)
						};

					if (response.LoggedOnUser.UserName == "administrator")
					{
						claims.Add(new Claim(ClaimTypes.Role, "Admin"));
					}

					var claimsIdentity = new ClaimsIdentity(
						claims,
						CookieAuthenticationDefaults.AuthenticationScheme);

					await HttpContext.SignInAsync(
						CookieAuthenticationDefaults.AuthenticationScheme,
						new ClaimsPrincipal(claimsIdentity));

					return RedirectToLocal(returnUrl);
				}
				else
				{
					return View();
				}
			}
			else
			{
				return View();
			}
		}


		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterUserViewModel model)
		{
			if (model != null)
			{
				var result = _apiGateway.PostAsJsonAsync<HttpResponseMessage>("login/register", model).Result;

				if (result.IsSuccessStatusCode)
				{
					var claims = new List<Claim>
						{
							new Claim(ClaimTypes.Name, model.FirstName),
							new Claim(ClaimTypes.Surname , model.LastName),
							new Claim(ClaimTypes.Email , model.Email)
						};

					if (model.UserName == "administrator")
					{
						claims.Add(new Claim(ClaimTypes.Role, "Admin"));
					}

					var claimsIdentity = new ClaimsIdentity(
						claims,
						CookieAuthenticationDefaults.AuthenticationScheme);

					await HttpContext.SignInAsync(
						CookieAuthenticationDefaults.AuthenticationScheme,
						new ClaimsPrincipal(claimsIdentity));

					return RedirectToAction("Index" , "Home");

				}
				else
				{
					return View();
				}
			}
			else
			{
				return View();
			}
		}

		public IActionResult Logout()
		{
		
			Response.Cookies.Delete(".AspNetCore.Cookies");

			return RedirectToAction("Login" , "Accounts");
		}

		private IActionResult RedirectToLocal(string returnUrl)
		{
			if (Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}
			else
			{
				return RedirectToAction(nameof(HomeController.Index), "Home");
			}
		}
	

	}
}