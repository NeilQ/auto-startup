using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceProcess;

namespace AutoStartup
{
    public partial class AutoStartupService : ServiceBase
    {
        private ProcessController _controller;

        public AutoStartupService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var config = ConfigurationManager.GetSection("autoStartup") as StartupSection;

            _controller = new ProcessController(config);
            _controller.Start();
        }

        protected override void OnStop()
        {
            _controller?.Stop();
        }

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }
    }
}
