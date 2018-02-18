using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.Entity;
using MySqlTest.Console.Migrations;

namespace MySqlTest.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new SellRightContext();
            context.Database.CreateIfNotExists();
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SellRightContext, Configuration>());
        }
    }
    class Product
    {
        public int ProductId
        {
            get;
            set;
        }
        public string ProductName
        {
            get;
            set;
        }
        public double Price
        {
            get;
            set;
        }
        public int Quantity
        {
            get;
            set;
        }
    }

    // Code-Based Configuration and Dependency resolution  
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    class SellRightContext : DbContext
    {
        //Add your Dbsets here  
        public DbSet<Product> Products
        {
            get;
            set;
        }
        public SellRightContext()
            //Reference the name of your connection string  
            : base("PriceConnection") { }
    }

    public class MyConfiguration : MySqlEFConfiguration
    {
        public MyConfiguration()
        {
            SetDatabaseInitializer(new CreateDatabaseIfNotExists<SellRightContext>());
            SetDatabaseInitializer(new MigrateDatabaseToLatestVersion<SellRightContext, Configuration>());
        }
    }
}
