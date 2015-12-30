using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceProcess;

namespace AutoStartup
{
    public partial class AutoStartupService : ServiceBase
    {
        private ProcessController _controller;
        private readonly Logger _logger = new Logger();

        public AutoStartupService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            StartupSection config = null;
            try
            {
                config = ConfigurationManager.GetSection("autoStartup") as StartupSection;
            }
            catch (Exception e)
            {
                _logger.Log(e.Message + e.StackTrace);
                OnStop();
            }

            _controller = new ProcessController(config, _logger);
            _controller.Start();
        }

        protected override void OnStop()
        {
            _controller?.Stop();
        }

        internal void TestStartupAndStop(string[] args)
        {
            OnStart(args);
            Console.ReadLine();
            OnStop();
        }
    }
}
