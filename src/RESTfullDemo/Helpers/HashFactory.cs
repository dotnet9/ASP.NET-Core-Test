using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;

namespace RESTfullDemo.Helpers
{
    public class HashFactory
    {
        public static string GetHash(object entity)
        {
            string result = string.Empty;
            var json = JsonConvert.SerializeObject(entity);
            var bytes = Encoding.UTF8.GetBytes(json);

            using(var hasher = MD5.Create())
            {
                var hash = hasher.ComputeHash(bytes);
                result = BitConverter.ToString(hash);
                result = result.Replace("-", "");
            }

            return result;
        }
    }
}
