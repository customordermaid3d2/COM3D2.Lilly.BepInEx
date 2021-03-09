using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using BepInEx;
using BepInEx.Logging;

namespace COM3D2.Lilly.Plugin
{
    static class MyLog 
    {
        static ManualLogSource log = BepInEx.Logging.Logger.CreateLogSource("Lilly");

        internal static void Log(string v)
        {
            //Debug.Log("Lilly:"+v);
            //Console.ForegroundColor = ConsoleColor.Green;//베핀쪽에서 색설정 막아버리는듯
            //Console.WriteLine("■Lilly: " + v+ " ■");
            //Debug.Log("■Lilly: " + v+ " ■");
            log.LogMessage("■ " + v); // [Message:COM3D2.Lilly.Plugin] ■Lilly: Plugin() ■
            //Console.ResetColor();
            // Logger.LogInfo("This is information");
            // Logger.LogWarning("This is a warning");
            // Logger.LogError("This is an error");
        }

        internal static void LogError(string v)
        {

            //Debug.LogError("■Lilly: " + v+ " ■");
            log.LogError("■ " + v);

        }
    }
}
