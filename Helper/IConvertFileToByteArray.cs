using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem2.Helper
{
    public interface IConvertFileToByteArray
    {
        byte[] Convert(IFormFile file);
    }
}
