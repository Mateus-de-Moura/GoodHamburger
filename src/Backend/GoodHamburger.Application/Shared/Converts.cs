using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Application.Shared
{
    public static class Converts
    {       
        public static async Task<byte[]> FormFileToByteArrayAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Array.Empty<byte>();

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            return memoryStream.ToArray();
        }

        public static string ByteArrayToBase64(byte[] fileBytes)
        {
            if (fileBytes == null || fileBytes.Length == 0)
                return string.Empty;

            return Convert.ToBase64String(fileBytes);
        }
    }
}
