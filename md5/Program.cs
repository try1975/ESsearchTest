using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace md5
{
    internal static class Program
    {
        private static void Main()
        {
            var encoding = Encoding.UTF8;
            const string key = "longencryptionkey";
            const string uRI = "http://laitmed.ru/soft-zellin-spirtovye-salfetki-60-mm-h-30-mm";
            const string name = "SOFT-ZELLIN-Спиртовые салфетки 60 мм х 30 мм  ";
            var str = $"|name|{name}|uRI|{uRI}|";
            var keyBytes = encoding.GetBytes(key);
            var hash = encoding.GetBytes(str);
            using (var md5 = new HMACMD5(keyBytes))
            {
                var hashenc = md5.ComputeHash(hash);
                var result = hashenc.Aggregate("", (current, b) => current + b.ToString("x2"));
                Console.WriteLine(result);
                const string defaultId = "a82790775c312f18edabd36e99787300";
                Console.WriteLine(defaultId);
                Console.WriteLine(defaultId.Equals(result));
            }
            Console.ReadKey();
        }
    }
}
