using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Coach
{
    class Hash
    {
        public string GetHash(string _filepath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(_filepath))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }

        }
    }
}
