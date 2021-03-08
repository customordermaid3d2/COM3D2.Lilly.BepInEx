using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.BepInEx
{
    static class MyLog
    {

        internal static void Log(string v)
        {
            //Debug.Log("Lilly:"+v);
            //Console.ForegroundColor = ConsoleColor.Green;//베핀쪽에서 색설정 막아버리는듯
            Console.WriteLine("■Lilly: " + v+ " ■");
            //Console.ResetColor();
            // Logger.LogInfo("This is information");
            // Logger.LogWarning("This is a warning");
            // Logger.LogError("This is an error");
        }
    }
}
