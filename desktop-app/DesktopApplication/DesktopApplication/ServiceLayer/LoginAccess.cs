using DesktopApplication.ModelLayer;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApplication.ServiceLayer
{
    public class LoginAccess : ILoginAccess
    {

        private readonly RestClient _restClient;
        public LoginAccess()
        {
            _restClient = new RestClient("https://localhost:7200/");
        }

        public async Task<string?> GetToken(string username, string password)
        {
            var request = new RestRequest("token");
            request.AddParameter("username", username);
            request.AddParameter("password", password);
            var response = await _restClient.ExecutePostAsync<string>(request);

            if(response.IsSuccessful)
            {
                return response.Data;
            }
            else
            {
                return "error";
            }
        }
    }
}
