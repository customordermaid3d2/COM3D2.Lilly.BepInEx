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
        /// 가상화에서 통합적으론 안되는듯?
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="___is_enabled"></param>
        //[HarmonyPatch(typeof(AbstractFreeModeItem), "is_enabled", MethodType.Getter)]
        //[HarmonyPostfix]//HarmonyPostfix ,HarmonyPrefix
        public static void get_is_enabled(AbstractFreeModeItem __instance, bool __result)
        {
            __result = true;
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
        
        [HarmonyPatch(typeof(FreeModeItemEveryday), "is_enabled", MethodType.Getter)]
        [HarmonyPostfix]//HarmonyPostfix ,HarmonyPrefix
        public static void get_is_enabled(FreeModeItemEveryday __instance, bool __result)
        {
            __result = true;
            FreeModeItemEveryday data = __instance;
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

        [HarmonyPatch(typeof(FreeModeItemLifeMode), "is_enabled", MethodType.Getter)]
        [HarmonyPostfix]//HarmonyPostfix ,HarmonyPrefix
        public static void get_is_enabled(FreeModeItemLifeMode __instance, bool __result)
        {
            __result = true;
            FreeModeItemLifeMode data = __instance;
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

        [HarmonyPatch(typeof(FreeModeItemVip), "is_enabled", MethodType.Getter)]
        [HarmonyPostfix]//HarmonyPostfix ,HarmonyPrefix
        public static void get_is_enabled(FreeModeItemVip __instance, bool __result)
        {
            __result = true;
            FreeModeItemVip data = __instance;
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
