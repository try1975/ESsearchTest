using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using HakkauWebScreenshot;

namespace Webshots_01
{
    class Program
    {
        static void Main(string[] args)
        {
            var job= new Screenshot().capture(args[0]);
            job.PerformCapture();
            using (var image = Image.FromStream(new MemoryStream(job.Output)))
            {
                image.Save($"{args[1]}", ImageFormat.Jpeg);  // Or Png
            }
        }
    }
}
