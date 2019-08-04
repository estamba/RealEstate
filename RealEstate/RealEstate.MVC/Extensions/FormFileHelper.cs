using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.MVC.Extensions
{
    public static class FormFileHelper
    {
        public static byte[] ToByteArray(this IFormFile formFile)
        {
            using (var ms = new MemoryStream())
            {
                formFile.CopyTo(ms);

                var byteArray = ms.ToArray();
                return byteArray;
            }
        }
        public static async Task<byte[]> ToByteArrayAsync(this IFormFile formFile)
        {
            using (var ms = new MemoryStream())
            {
                await formFile.CopyToAsync(ms);
                return ms.ToArray();
            }
        }
    }
}
