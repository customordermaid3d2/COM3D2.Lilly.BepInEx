using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    class FreeModeItemLifeModePatch
    {
        // FreeModeItemLifeMode


        [HarmonyPatch(typeof(FreeModeItemLifeMode), "is_enabled", MethodType.Getter)]
        [HarmonyPostfix]//HarmonyPostfix ,HarmonyPrefix
        public static void get_is_enabled( bool __result)
        {
            __result = true;
            AbstractFreeModeItemPatch.OutMsg("FreeModeItemLifeMode.get_is_enabled");
        }

    }
}
