using CM3D2.ExternalSaveData.Managed;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    /// <summary>
    /// ExSaveData
    /// </summary>
    class ExSaveDataPatch
    {
        // public static bool Set(Maid maid, string pluginName, string propName, string value, bool overwrite)
        [HarmonyPatch(typeof(ExSaveData), "Set"), HarmonyPostfix]
        public static void Set(bool __result, Maid maid, string pluginName, string propName, string value, bool overwrite)
        {
            MyLog.LogMessage(
                "ExSaveData.Set"
                ,MaidUtill.GetMaidFullNale(maid)
                , pluginName
                , propName
                , value
                , overwrite
                , __result
                );
        }
    }
}
