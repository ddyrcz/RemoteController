﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugins
{
    public class SystemBeep : IPlugin
    {
        public new ActionType GetType()
        {
            return ActionType.SystemBeep;
        }

        public Guid GetGuid()
        {
            return Guid.NewGuid();
        }

        public void Invoke()
        {
            Console.Beep();
        }

        public int GetPriority()
        {
            return 1;
        }
    }
}
