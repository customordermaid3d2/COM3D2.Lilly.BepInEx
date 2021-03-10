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
        string name;

        public MyLog()
        {
            this.name = this.GetType().Name;
        }

        public MyLog(string name)
        {
            this.name = name;
        }

        public void LogMessage(string v)
        {
            LogMessageS(name+" : "+v);
        }
        internal static void LogMessageS(string v)
        {
            //Debug.Log("Lilly:"+v);
            //Console.ForegroundColor = ConsoleColor.Green;//베핀쪽에서 색설정 막아버리는듯
            //Console.WriteLine("■Lilly: " + v+ " ■");
            //Debug.Log("■Lilly: " + v+ " ■");
            log.LogMessage( v); // [Message:COM3D2.Lilly.Plugin] ■Lilly: Plugin() ■
            //Console.ResetColor();
            // Logger.LogInfo("This is information");
            // Logger.LogWarning("This is a warning");
            // Logger.LogError("This is an error");
        }

        internal static void LogWarningS(string v)
        {
            log.LogWarning(v);
        }
        internal static void LogInfoS(string v)
        {
            log.LogInfo(v);
        }
        internal static void LogFatalS(string v)
        {
            log.LogFatal(v);
        }
        internal static void LogDebugS(string v)
        {
            log.LogDebug(v);
        }

        internal static void LogErrorS(string v)
        {

            //Debug.LogError("■Lilly: " + v+ " ■");
            log.LogError( v);

        }

        internal void LogError(string v)
        {
            LogErrorS(name + " : " + v);
        }
    }
}
