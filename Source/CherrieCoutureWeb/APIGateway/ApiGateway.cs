using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;
using CherrieCoutureWeb.APIGateway.ResponseModels;
using ServiceStack;

namespace CherrieCoutureWeb.APIGateway
{
    public class ApiGateway
    {
		private JsonServiceClient _JsonClient;

		public ApiGateway()
		{
			_JsonClient = new JsonServiceClient("http://cherriecouture.stormsimmons.local/api/");
		}

		public async Task<T> PostAsJsonAsync<T>(string requestUri, object Value)
		{
			var response = await _JsonClient.PostAsync<T>(requestUri, Value);
			return response;
		}

		public async Task<T> GetAsJsonAsync<T>(string requestUri)
		{
			var response = await _JsonClient.GetAsync<T>(requestUri);
			return response;
		}
    }
}
