using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AutoStartup
{
    public class ProcessController
    {
        private readonly StartupSection _config;
        private readonly ILogger _logger;
        private IList<Process> _processes;

        public ProcessController(StartupSection copnfig, ILogger logger)
        {
            _config = copnfig;
            _logger = logger;
        }

        public void Start()
        {
            if (_config?.Startups == null || _config.Startups.Count == 0) return;

            _processes = new List<Process>();
            try
            {
                foreach (StartupElement t in _config.Startups)
                {
                    var info = new ProcessStartInfo(t.Path)
                    {
                        UseShellExecute = false,
                        RedirectStandardError = true,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true,
                        ErrorDialog = false,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        Arguments = t.Args
                    };

                    _processes.Add(Process.Start(info));
                }
            }
            catch (Exception e)
            {
                _logger.Log(e.Message + e.StackTrace);
            }
        }

        public void Stop()
        {
            try
            {

                foreach (var process in _processes)
                {
                    if (process != null && !process.HasExited)
                    {
                        process.Kill();
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Log(e.Message + e.StackTrace);
            }
            finally
            {
                _processes = null;
            }
        }
    }
}