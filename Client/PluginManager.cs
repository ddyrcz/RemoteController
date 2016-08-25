using Common;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Client
{
    internal static class PluginManager
    {
        public static IEnumerable<IPlugin> GetAvailablePlugins()
        {
            Assembly assembly = Assembly.Load("Plugins");

            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetInterface(typeof(IPlugin).Name) != null)
                {
                    yield return (IPlugin)Activator.CreateInstance(type);
                }
            }
        }
    }
}