using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueTapeCrew.Interfaces
{
    public interface IWebService
    {
        string FormatAuthorizationCredentials(string username, string password);
    }
}
