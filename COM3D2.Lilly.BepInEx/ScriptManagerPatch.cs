using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    class ScriptManagerPatch
    {
        // public void ExecScriptFile(string file_name, ref TJSVariant result)
        [HarmonyPatch(typeof(ScriptManager), "ExecScriptFile")]
        [HarmonyPostfix]
        static void IsExecStagePost(string file_name, ref TJSVariant result, ScriptManager __instance)
        {
            MyLog.Log("ScriptManager.IsExecStagePost:" + file_name);
        }
    }
}
