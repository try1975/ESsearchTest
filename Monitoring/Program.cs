using System.Threading;

namespace Monitoring
{
    class Program
    {
        static void Main()
        {
            MonitoringScheduleChecker.Execute();
            Thread.Sleep(10000);
        }
    }
}
