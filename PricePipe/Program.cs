using System;
using System.Configuration;
using System.IO;
using System.Linq;
using PricePipeCore;

namespace PricePipe
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 0) return;
            var names = File.ReadAllLines(args[0]);
            if (names.Any())
            {
                var defaultIndex = ConfigurationManager.AppSettings["ElasticSearchDefaultIndex"];
                var address = ConfigurationManager.AppSettings["ElasticSearchAddress"];
                var userName = ConfigurationManager.AppSettings["ElasticSearchUserName"];
                var password = ConfigurationManager.AppSettings["ElasticSearchPassword"];
                
                var priceCalculator = new Searcher(defaultIndex, address, userName, password);
                foreach (var name in names)
                {
                    var nmck = priceCalculator.GetNmck(name);
                    Console.WriteLine($"\"{name}\";\"{nmck}\"");
                }
            }
            Console.ReadKey();
        }
    }
    
}
