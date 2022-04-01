using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem2.Authentication
{
    public interface IAuthenticate
    {
        bool AuthenticateUser(string matricNo, string password);
        bool CompareHash(string hash, string password);
        string GenerateToken(string matricNo);
        string GenerateHash(string password);
    }
}
