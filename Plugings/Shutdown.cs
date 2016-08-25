using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugins
{
    public class Shutdown : IPlugin
    {
        public new ActionType GetType()
        {
            return ActionType.Shutdown;
        }

        public Guid GetGuid()
        {
            return Guid.NewGuid();
        }

        public void Invoke()
        {
            var initProcess = new ProcessStartInfo("Shutdown", "-s -t 0");
            initProcess.CreateNoWindow = true;
            initProcess.UseShellExecute = false;
            Process.Start(initProcess);
        }

        public int GetPriority()
        {
            return 1;
        }
    }
}
