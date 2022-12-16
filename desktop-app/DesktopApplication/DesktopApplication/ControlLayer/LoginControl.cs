using DesktopApplication.ModelLayer;
using DesktopApplication.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApplication.ControlLayer
{
    public class LoginControl
    {
        readonly ILoginAccess _loginAccess;

        public LoginControl()
        {
            _loginAccess = new LoginAccess();
        }

        public async Task<string?> GetToken(string username, string password)
        {
            return await _loginAccess.GetToken(username, password);
        }
    }
}
