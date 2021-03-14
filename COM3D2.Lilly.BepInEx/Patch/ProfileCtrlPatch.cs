using HarmonyLib;
using MaidStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    /// <summary>
    /// 성격 정보가 있나?
    /// </summary>
    class ProfileCtrlPatch
    {
        /// <summary>
        ///  초기화
        /// </summary>
        /// <param name="__instance"></param>
        [HarmonyPatch(typeof(ProfileCtrl), "Init")]
        [HarmonyPostfix]
        private static void Init(ProfileCtrl __instance, Status ___m_maidStatus) // string __m_BGMName 못가져옴
        {
            MyLog.LogMessageS("ProfileCtrl.Init:");
        }
    }
}
