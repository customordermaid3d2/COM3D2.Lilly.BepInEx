using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    class FreeModeItemVipPatch
    {
        // FreeModeItemVip

        /// <summary>
        /// AbstractFreeModeItemPatch 와 중복됨
        /// </summary>
        /// <param name="__result"></param>
        [HarmonyPatch(typeof(FreeModeItemVip), "is_enabled", MethodType.Getter)]
        [HarmonyPrefix]//HarmonyPostfix ,HarmonyPrefix
        public static void is_enabled( bool __result)
        {
            __result = true;
            AbstractFreeModeItemPatch.OutMsg("FreeModeItemVip.is_enabled");
        }

    }
}
