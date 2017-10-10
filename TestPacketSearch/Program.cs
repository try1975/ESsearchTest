using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TestPacketSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            var jsonText= File.ReadAllText(@"C:\Projects\GitHub\ESsearchTest\TestPacketSearch\test.json");
            var searchItems = JsonConvert.DeserializeObject<List<SearchItem>>(jsonText);
            foreach (var searchItem in searchItems)
            {
                var json = JsonConvert.SerializeObject(searchItem);
                Console.WriteLine(json);
            }
        }
    }
}
