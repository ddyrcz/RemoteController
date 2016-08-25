using Common;
using System;

namespace Plugins
{
    internal class LongSystemBeep : IPlugin
    {
        public Guid GetGuid()
        {
            return Guid.NewGuid();
        }

        public int GetPriority()
        {
            return 5;
        }

        public new ActionType GetType()
        {
            return ActionType.SystemBeep;
        }

        public void Invoke()
        {
            System.Threading.Thread.Sleep(500);
            Console.Beep(15000, 2000);
        }
    }
}