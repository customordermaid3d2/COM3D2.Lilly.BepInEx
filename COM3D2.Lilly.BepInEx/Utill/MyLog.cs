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
            action(MyUtill.Join(" , ", args));
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
