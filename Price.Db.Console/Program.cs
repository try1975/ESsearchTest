using System.Linq;
using System.Threading;
using Price.Db.Postgress;
using Price.Db.Postgress.QueryProcessors;

namespace Price.Db.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new PriceContext();
            var searchItemQuery = new SearchItemQuery(context);
            foreach (var searchItemEntity in searchItemQuery.GetEntities().ToList())
            {
                System.Console.WriteLine($@"id={searchItemEntity.Id}");
            }
            System.Console.WriteLine(@"Complete.");
            Thread.Sleep(3000);
        }
    }
}
