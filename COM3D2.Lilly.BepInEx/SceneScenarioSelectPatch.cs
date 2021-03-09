using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    /// <summary>
    /// 이벤트 설정 화면
    /// </summary>
    public static class SceneScenarioSelectPatch
    {
        [HarmonyPatch(typeof(SceneScenarioSelect), "OnSelectScenario")]
        [HarmonyPostfix]
        private static void OnSelectScenarioPost(UILabel ___m_ContentsLabel, string ___m_JumpLabel) // string __m_BGMName 못가져옴
        {
            MyLog.Log("OnSelectScenarioPost:"+ ___m_ContentsLabel.text + "," + ___m_JumpLabel);
            //MyLog.Log("OnSelectScenarioPost:" + __m_BGMName);
        }
    }
}
