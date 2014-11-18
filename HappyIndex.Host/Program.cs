using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace HappyIndex.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = getSiteUrl(args);

            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Running on {0}", url);
                Console.WriteLine("Press enter to exit");
                Console.ReadLine();
            }
        }

        static string getSiteUrl(string[] args)
        {
            var defaultPort = "8080";
            var port = args.Length == 1 ? args[0] : defaultPort;
            return string.Format("http://+:{0}", port);
        }
    }
}
