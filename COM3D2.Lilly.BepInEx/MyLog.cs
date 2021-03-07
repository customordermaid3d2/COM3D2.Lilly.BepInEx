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
            Debug.Log("Lilly:"+v);
            // Logger.LogInfo("This is information");
            // Logger.LogWarning("This is a warning");
            // Logger.LogError("This is an error");
        }
    }
}
