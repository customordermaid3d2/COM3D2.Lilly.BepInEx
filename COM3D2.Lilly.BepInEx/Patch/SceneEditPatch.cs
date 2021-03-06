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
        /// <summary>
        /// 오류나서 제거
        /// </summary>
        /// <param name="___m_maid"></param>
        /// <param name="__instance"></param>
        //private void Start()
        //private Maid m_maid;
        [HarmonyPatch(typeof(SceneEdit), "Start")]
        [HarmonyPostfix]
        public static void Start() // Maid ___m_maid,SceneEdit __instance
        {            
            Maid ___m_maid = SceneEdit.Instance.maid;
            if (___m_maid == null)
            {
                MyLog.LogError("SceneEdit.Start:null");
                return;
            }
            MyLog.LogMessage("SceneEdit.Start:" + ___m_maid.status.charaName.name1 + " , " + ___m_maid.status.charaName.name2);
            MaidStatusUtill.SetMaidStatus(___m_maid);
        }

        //[HarmonyPatch(typeof(SceneEdit.MenuItemSet), "Start")]
        //[HarmonyPostfix]
        //public static void StartPos() // Maid ___m_maid,SceneEdit __instance
        //{            
        //    Maid ___m_maid = SceneEdit.Instance.maid;
        //    if (___m_maid == null)
        //    {
        //        MyLog.LogErrorS("SceneEdit.Start:null");
        //        return;
        //    }
        //    MyLog.LogMessageS("SceneEdit.Start:" + ___m_maid.status.charaName.name1 + " , " + ___m_maid.status.charaName.name2);
        //    MaidStatusUtill.SetMaidStatus(___m_maid);
        //}
    }
}
