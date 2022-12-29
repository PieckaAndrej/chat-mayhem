using RestSharp;

namespace WebApp.Services
{
    public class LoginAccess
    {
        public static string token;

        public LoginAccess()
        {

        }

        public static async Task<string?> GetToken(string username, string password)
        {
            string serviceUrl = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json", optional: false)
                                .Build().GetSection("ServiceURL").Value;

            RestClient _restClient = new RestClient(serviceUrl);
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
