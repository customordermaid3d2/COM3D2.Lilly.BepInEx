using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using BepInEx;
using BepInEx.Logging;

namespace COM3D2.Lilly.Plugin
{
    class MyLog 
    {
        static ManualLogSource log = BepInEx.Logging.Logger.CreateLogSource("Lilly");

        private static void LogOut(object[] args, Action<string> action)
        {
            if (!Lilly.isLogOnOffAll)
                return;
            if (args.Length==0)
            {
                action("");
                return;
            }

            StringBuilder s = new StringBuilder();
            //s.Append(name + " , ");
            s.Append(args[0].ToString());
            for (int i = 1; i < args.Length; i++)
            {
                s.Append(" , " + args[i].ToString());
            }
            action(s.ToString());
        }

        internal static void LogMessage(params object[] args)
        {
            LogOut(args, log.LogMessage);
        }

        internal static void LogWarning(params object[] args)
        {
            LogOut(args, log.LogWarning);
        }

        internal static void LogInfo(params object[] args)
        {
            LogOut(args, log.LogInfo);
        }

        internal static void LogFatal(params object[] args)
        {
            LogOut(args, log.LogFatal);
        }

        internal static void LogDebug(params object[] args)
        {
            LogOut(args, log.LogDebug);
        }

        internal static void LogError(params object[] args)
        {
            LogOut(args, log.LogError);
        }

    }
}
