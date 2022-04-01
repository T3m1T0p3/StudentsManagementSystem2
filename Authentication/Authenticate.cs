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
            Console.WriteLine("Generating hash");
            if (password==null || password.Length<8 || password.Length > 12)
            {
                throw new Exception("Pasword must between between 8 and 12 characters");
            }
            Console.WriteLine("Generating hash");
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
            if (BCryptNet.Verify(hash, password))
             {
                return true;
             }
            return false;
        }
        private readonly byte[] secret = Encoding.ASCII.GetBytes("ourlittlesecret");
        public string GenerateToken(string matricNo)
        {
            var tokenHandler=new JwtSecurityTokenHandler();
            var now = DateTime.Now;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, matricNo) }),
                Expires = now.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.Aes256Encryption)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            return token;
        }
    }
}
