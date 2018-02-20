using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireAndForget
{
    class Program
    {
        static string connectionString = "data source=.;initial catalog=test;integrated security=True";

        //STEP ONE: Create a delegate whose signature matches the method to be called

        delegate void InsertDelegate(string first, string second, int num);
        // store our test iterations number
        static int numIterations = 100000;


        [STAThread]
        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;
            DateTime done;
            //STEP TWO: Create a new instance of your delegate, pointing to your target method
            Delegate dWrite = new InsertDelegate(WriteIt);

            for (int i = 0; i < numIterations; i++)
            {
                //STEP THREE: Execute FireAndForget helper method,passing the Delegate instance,
                // and then passing the method parameters as an object array
                ThreadUtil.FireAndForget(dWrite, new object[] { "hoohoo", "hahaa", i });
            }

            done = DateTime.Now;
            TimeSpan elapsed = done - start;
            double totTime = elapsed.TotalMilliseconds;
            double avgTime = totTime / numIterations;
            Console.WriteLine(elapsed.TotalMilliseconds.ToString());
            Console.WriteLine("Average time to Queue each WorkItem : " + avgTime.ToString());
            Console.WriteLine("Wait for \"DONE.\",then Press any key to quit.");
            Console.ReadLine();
        }

        static void WriteIt(string first, string second, int num)
        {

            SqlConnection cn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "dbo.InsertTblTest";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@First", first));
            cmd.Parameters.Add(new SqlParameter("@Second", second));
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

                cn.Close();
                cmd.Dispose();
            }
            if (num == numIterations - 1) Console.WriteLine("DONE.");
        }
    }
}
