using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    /// <summary>
    /// 포토모드 팝업창 관련
    /// </summary>
    class PopupAndTabListPatch : MyLog// PopupAndTabList
    {
        static string name = "PopupAndTabList";

        //public void SetData(Dictionary<string, List<KeyValuePair<string, object>>> popup_and_button_name_list, Dictionary<string, List<string>> buttonTermList, bool create_margin = false)
        [HarmonyPatch(typeof(PopupAndTabList), "SetData")]
        [HarmonyPostfix]
        private static void SetDataPost0(PopupAndTabList __instance) // string __m_BGMName 못가져옴
        {
            LogMessageS(name+ ".SetData" );
            //MyLog.Log("OnSelectScenarioPost:" + __m_BGMName);
        }
    }
}
