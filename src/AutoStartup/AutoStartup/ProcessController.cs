using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AutoStartup
{
    public class ProcessController
    {
        private readonly IList<string> _processPaths;
        private IList<Process> _processes;

        public ProcessController(IList<string> processPaths)
        {
            _processPaths = processPaths;

        }

        public void Start()
        {
            if (_processPaths == null || _processPaths.Count == 0) return;

            _processes = new List<Process>();
            try
            {
                foreach (var t in _processPaths)
                {
                    var info = new ProcessStartInfo(t)
                    {
                        UseShellExecute = false,
                        RedirectStandardError = true,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true,
                        ErrorDialog = false,
                        WindowStyle = ProcessWindowStyle.Hidden,
                    };

                    _processes.Add(Process.Start(info));
                }
            }
            catch (Exception e)
            {
                // ignore
                // todo: log
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
            catch (Exception)
            {
                // ignore
                // todo: log
            }
            finally
            {
                _processes = null;
            }
        }
    }
}