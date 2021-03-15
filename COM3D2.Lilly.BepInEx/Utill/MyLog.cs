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
        
        internal static void LogMessageS(params object[] args)
        {
            if (!Lilly.isLogOnOffAll)
                return;
            StringBuilder s = new StringBuilder(args[0].ToString());
            object[] t=new object[args.Length-1];
            args.CopyTo(t, 1);
            foreach (var i in t)
            {
                s.Append(" , "+i);
            }
            Debug.Log("Lilly:" + s);
            // s.Append(args[1..]); //8.0 문법;;
            // s.Append(args[1..]); //8.0 문법;;
        }


        internal static void LogMessageS(string v)
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

        internal static void LogWarningS(string v)
        {
            if (Lilly.isLogOnOffAll)
                log.LogWarning(v);
        }
        internal static void LogInfoS(string v)
        {
            if (Lilly.isLogOnOffAll)
                log.LogInfo(v);
        }
        internal static void LogFatalS(string v)
        {
            if (Lilly.isLogOnOffAll)
                log.LogFatal(v);
        }
        internal static void LogDebugS(string v)
        {
            if (Lilly.isLogOnOffAll)
                log.LogDebug(v);

        }

        internal static void LogErrorS(string v)
        {

            //Debug.LogError("■Lilly: " + v+ " ■");
            if (Lilly.isLogOnOffAll)
                log.LogError( v);

        }

        internal void LogError(string v)
        {
            if (Lilly.isLogOnOffAll)
                LogErrorS(name + " : " + v);
        }
    }
}
