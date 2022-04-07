using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using StudentManagementSystem2.Entity;
using StudentManagementSystem2.StudentContexts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;


namespace StudentManagementSystem2.Authentication
{
    public class Authenticate: IAuthenticate
    {
        public Authenticate()
        {
            //_context = context;
        }
        public string GenerateHash(string password)
        {

            if (password==null || password.Length<8 || password.Length > 12)
            {
                throw new Exception("Pasword must between between 8 and 12 characters");
            }
            string hash=BCryptNet.HashPassword(password);
            Console.WriteLine(hash);
            return hash;
        }
        public bool AuthenticateUser(string hash, string password)
        { 
            if (CompareHash(hash, password))
            {
                return true;
            }
            return false;
        }

        public bool CompareHash(string hash,string password)
        {

            if (BCryptNet.Verify(password, hash)) 
             {
                return true;
             }
            return false;
        }
        private readonly byte[] secret = Encoding.ASCII.GetBytes("abcdefghijklmnop");
        public string GenerateToken(string matricNo)
        {
            var tokenHandler=new JwtSecurityTokenHandler();
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, matricNo) }),
                Expires =DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            return token;
        }
    }
}
