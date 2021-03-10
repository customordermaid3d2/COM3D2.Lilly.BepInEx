using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    class MaidManagementMainPatch
    {
        [HarmonyPatch(typeof(MaidManagementMain), "OnSelectChara")]
        [HarmonyPostfix]
        public static void OnSelectChara(Maid ___select_maid_, Dictionary<string, UIButton> ___button_dic_, MaidManagementMain __instance)
        {
            MyLog.LogMessageS("SceneEdit.Start:" + ___select_maid_.status.charaName.name1 + " , " + ___select_maid_.status.charaName.name2);

            MaidStatus.SetMaidStatus(___select_maid_);
            //___m_maid.status.base = 9999;
            //___m_maid.status.base = 9999;

            foreach (var item in ___button_dic_)
            {
                item.Value.isEnabled = true;
            }
        }

    }
}
