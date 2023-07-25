using System;
using System.ServiceProcess;

namespace Jaxis.Engine
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new EngineService() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
