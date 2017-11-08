using CherrieCoutureWeb.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CherrieCoutureWeb.APIGateway.ResponseModels
{
    public class LoginValidationResponse
    {
		public User LoggedOnUser { get; set; }
		public bool ValidCredentials { get; set; }
	}
}
