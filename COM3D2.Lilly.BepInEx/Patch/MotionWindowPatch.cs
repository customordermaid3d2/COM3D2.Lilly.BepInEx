using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    /// <summary>
    /// 포토모드 모션창
    /// </summary>
    internal class MotionWindowPatch : MyLog
    {
        //public override void Awake()
        [HarmonyPatch(typeof(MotionWindow), "Awake")]
        [HarmonyPostfix]
        private static void AwakePost0(MotionWindow __instance) // string __m_BGMName 못가져옴
        {
            LogMessageS("MotionWindow.AwakePost0");
            //MyLog.Log("OnSelectScenarioPost:" + __m_BGMName);
        }
    }
}
