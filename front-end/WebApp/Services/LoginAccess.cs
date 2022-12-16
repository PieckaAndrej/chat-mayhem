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
