using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApplication.ServiceLayer
{
    public interface ILoginAccess
    {
        Task<string?> GetToken(string username, string password);
    }
}
