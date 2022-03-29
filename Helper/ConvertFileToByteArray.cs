using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem2.Helper
{
    public class ConvertFileToByteArray:IConvertFileToByteArray
    {
        //private readonly IFormFile _file;

        /*public ConvertFileToByteArray(IFormFile file)
        {
            Console.WriteLine("Instance created");
            _file = file;
        }*/
        public byte[] Convert(IFormFile file) {
            if (file != null)
            {
                Console.WriteLine(" byte??");
                var stream = new MemoryStream();
                Console.WriteLine("Returning byte[]");
                file.CopyTo(stream);
                Console.WriteLine("Copied");
                return stream.ToArray();
            }
            return new byte[0];
            
        }
    }
}
