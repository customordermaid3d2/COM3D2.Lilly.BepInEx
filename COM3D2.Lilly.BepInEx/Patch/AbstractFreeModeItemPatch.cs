using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    class AbstractFreeModeItemPatch
    {
        // AbstractFreeModeItem

        /// <summary>
        /// FreeModeItemEveryday
        /// FreeModeItemLifeMode
        /// FreeModeItemVip
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="___is_enabled"></param>
        [HarmonyPatch(typeof(AbstractFreeModeItem), "get_is_enabled", MethodType.Getter)]
        [HarmonyPatch(typeof(FreeModeItemEveryday), "get_is_enabled", MethodType.Getter)]
        [HarmonyPatch(typeof(FreeModeItemLifeMode), "get_is_enabled", MethodType.Getter)]
        [HarmonyPatch(typeof(FreeModeItemVip), "get_is_enabled", MethodType.Getter)]
        [HarmonyPostfix]//HarmonyPostfix ,HarmonyPrefix
        public void get_is_enabled(AbstractFreeModeItem __instance, bool ___is_enabled)
        {
            ___is_enabled = true;
            AbstractFreeModeItem data = __instance;
            MyLog.LogMessage("AbstractFreeModeItem.get_is_enabled"
            , data.item_id
            , data.is_enabled
            , data.play_file_name
            , data.title
            , data.text
            , data.type
            , MyUtill.Join(" / ", data.condition_texts)
            , data.titleTerm
            , data.textTerm
            , MyUtill.Join(" , ", data.condition_text_terms)
            );
        }
    }
}
