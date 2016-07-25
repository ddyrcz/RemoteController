using System.Diagnostics;

namespace Client.Actions
{
    internal class Shutdown : IAction
    {
        public void Invoke()
        {
            var initProcess = new ProcessStartInfo("Shutdown", "-s -t 0");
            initProcess.CreateNoWindow = true;
            initProcess.UseShellExecute = false;
            Process.Start(initProcess);
        }
    }
}