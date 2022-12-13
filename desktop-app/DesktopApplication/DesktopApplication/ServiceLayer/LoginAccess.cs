using DesktopApplication.ModelLayer;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApplication.ServiceLayer
{
    public class LoginAccess : ILoginAccess
    {

        public static string token;

        public LoginAccess()
        {
            
        }

        public async Task<string?> GetToken(string username, string password)
        {
            RestClient _restClient = new RestClient("https://localhost:7200/");
            var request = new RestRequest("token");
            request.AddParameter("username", username);
            request.AddParameter("password", password);
            var response = await _restClient.ExecutePostAsync<string>(request);

            if (response.IsSuccessful)
            {
                token = response.Data;
                return token;
            }
            else
            {
                token = "";
                return "error";
            }
        }
    }
}
