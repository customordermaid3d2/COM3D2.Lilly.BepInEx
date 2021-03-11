using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    /// <summary>
    /// 메이드 에딧 모드로 들어갈시
    /// </summary>
    class SceneEditPatch
    {
        //private void Start()
        //private Maid m_maid;
        [HarmonyPatch(typeof(SceneEdit), "Start")]
        [HarmonyPostfix]
        public static void Start(Maid ___m_maid, SceneEdit __instance)
        {
            MyLog.LogMessageS("SceneEdit.Start:" + ___m_maid.status.charaName.name1 + " , " + ___m_maid.status.charaName.name2);

            MaidStatusPlugin.SetMaidStatus(___m_maid);
        }
    }
}
