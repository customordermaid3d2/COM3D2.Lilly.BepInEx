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


        internal static void LogMessage(string name,params object[] args)
        {
            if (!Lilly.isLogOnOffAll)
                return;
            StringBuilder s = new StringBuilder();
            s.Append(args[0].ToString());
            for (int i = 1; i < args.Length; i++)
            {
                s.Append(" , "+ args[i].ToString());
            }
            log.LogMessage(s);
            // s.Append(args[1..]); //8.0 문법;;
        }


        internal static void LogMessage(string v)
        {
            if (Lilly.isLogOnOffAll)
            {
            log.LogMessage( v); // [Message:COM3D2.Lilly.Plugin] ■Lilly: Plugin() ■
            }
            //Debug.Log("Lilly:"+v);
            //Console.ForegroundColor = ConsoleColor.Green;//베핀쪽에서 색설정 막아버리는듯
            //Console.WriteLine("■Lilly: " + v+ " ■");
            //Debug.Log("■Lilly: " + v+ " ■");
            //Console.ResetColor();
            // Logger.LogInfo("This is information");
            // Logger.LogWarning("This is a warning");
            // Logger.LogError("This is an error");
        }

        internal static void LogWarning(string v)
        {
            if (Lilly.isLogOnOffAll)
                log.LogWarning(v);
        }
        internal static void LogInfo(string v)
        {
            if (Lilly.isLogOnOffAll)
                log.LogInfo(v);
        }
        internal static void LogFatal(string v)
        {
            if (Lilly.isLogOnOffAll)
                log.LogFatal(v);
        }
        internal static void LogDebug(string v)
        {
            if (Lilly.isLogOnOffAll)
                log.LogDebug(v);

        }

        internal static void LogError(string v)
        {

            //Debug.LogError("■Lilly: " + v+ " ■");
            if (Lilly.isLogOnOffAll)
                log.LogError( v);

        }

    }
}
