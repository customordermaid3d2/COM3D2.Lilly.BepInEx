using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    class SceneADVPatch
    {
        // public void CallTitleBar(string title)
        [HarmonyPatch(typeof(SceneADV), "CallTitleBar")]
        [HarmonyPostfix]
        private static void CallTitleBarPost(SceneADV __instance, string title) // string __m_BGMName 못가져옴
        {
            MyLog.LogMessageS("CallTitleBarPost:" + title);
        }
    }
}
