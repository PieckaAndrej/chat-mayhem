using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PersonRestService.Security;
using System.IdentityModel.Tokens.Jwt;

namespace PersonRestService.Controllers
{
    public class TokensController : Controller
    {
        private readonly IConfiguration _configuration;

        public TokensController(IConfiguration inConfiguration)
        {
            _configuration = inConfiguration;
        }

        [Route("/token")]
        [HttpPost]
        public IActionResult Create(string username, string password)
        {

            bool hasInput = ((!String.IsNullOrWhiteSpace(username)) && (!String.IsNullOrWhiteSpace(password)));
            SecurityHelper secUtil = new SecurityHelper(_configuration);
            if (hasInput && secUtil.IsValidUsernameAndPassword(username, password))
            {
                string jwtToken = GenerateToken(username);
                return new ObjectResult(jwtToken);
            }
            else
            {
                return BadRequest();
            }
        }

        private string GenerateToken(string username)
        {
            string tokenString;

            SecurityHelper secUtil = new SecurityHelper(_configuration);

            SymmetricSecurityKey SIGNING_KEY = secUtil.GetSecurityKey();

            SigningCredentials credentials = new SigningCredentials(SIGNING_KEY, SecurityAlgorithms.HmacSha256);

            JwtHeader header = new JwtHeader(credentials);

            int ttlInMinutes = 10;
            DateTime expiry = DateTime.UtcNow.AddMinutes(ttlInMinutes);
            int ts = (int)(expiry - new DateTime(1970, 1, 1)).TotalSeconds;

            var payload = new JwtPayload {
                { "Name", username },
                { "exp", ts },
                { "iss", "https://localhost:7200" },
                { "aud", "https://localhost:7200" }  
            };

            JwtSecurityToken secToken = new JwtSecurityToken(header, payload);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            tokenString = handler.WriteToken(secToken);

            return tokenString;
        }

    }
}
