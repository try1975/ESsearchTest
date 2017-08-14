using System;
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
                var priceCalculator = new PharmacySearcher(AppSettings.DefaultIndex);

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