using System;
using System.Collections.Generic;
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
            var paths = new List<string>();
            foreach (StartupElement startup in StartupSection.Settings.Startups)
            {
                paths.Add(startup.Path);
            }

            _controller = new ProcessController(paths);
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
