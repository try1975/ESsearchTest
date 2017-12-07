using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Common.Dto
{
    public static class Md5Logstah
    {
        public static string GetDefaultId(string uRI, string name, string key = "longencryptionkey")
        {
            // look at fingerprint logstash plugin 
            // https://github.com/logstash-plugins/logstash-filter-fingerprint/blob/master/lib/logstash/filters/fingerprint.rb
            // line 134-138
            var encoding = Encoding.UTF8;
            var str = $"|{nameof(name)}|{name}|{nameof(uRI)}|{uRI}|";
            using (var md5 = new HMACMD5(encoding.GetBytes(key)))
            {
                var computeHash = md5.ComputeHash(encoding.GetBytes(str));
                return computeHash.Aggregate("", (current, b) => current + b.ToString("x2"));
            }
        }
    }
}