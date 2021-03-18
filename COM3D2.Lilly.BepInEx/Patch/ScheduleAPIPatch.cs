using HarmonyLib;
using Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    class ScheduleAPIPatch
    {
        // ScheduleAPI

        [HarmonyPatch(typeof(ScheduleAPI), "VisibleNightWork") ]
        [HarmonyPatch(typeof(ScheduleAPI), "EnableNightWork")]
        [HarmonyPatch(typeof(ScheduleAPI), "EnableNoonWork")]
        [HarmonyPrefix]//HarmonyPostfix ,HarmonyPrefix
        public static bool VisibleNightWork( out bool __result)
        {
            __result = true;
            MyLog.LogMessage("VisibleNightWork:"+ SceneFreeModeSelectManager.IsFreeMode);
            return SceneFreeModeSelectManager.IsFreeMode;
        }
    }
}
