using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    class EmpireLifeModeManagerPatch
    {
        // EmpireLifeModeManager

        [HarmonyPatch(typeof(EmpireLifeModeManager), "GetScenarioExecuteCount")]
        [HarmonyPostfix]//HarmonyPostfix ,HarmonyPrefix
        public static void GetScenarioExecuteCount(out int __result)
        {
            __result = 9999;
            MyLog.LogMessage("GetScenarioExecuteCount:");
        }
    }
}
