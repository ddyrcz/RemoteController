using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Actions
{
    class SystemBeep : IAction
    {
        public void Invoke()
        {
            Console.Beep();
        }
    }
}
