using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace AutoStartup
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {

            if (Environment.UserInteractive)
            {
                var service1 = new AutoStartupService();
                service1.TestStartupAndStop(new string[] { });
            }
            else
            {
                var servicesToRun = new ServiceBase[]
                {
                    new AutoStartupService()
                };
                ServiceBase.Run(servicesToRun);
            }
        }
    }
}
