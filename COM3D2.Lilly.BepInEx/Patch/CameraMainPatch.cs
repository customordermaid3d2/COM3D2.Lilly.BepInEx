using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin
{
    class CameraMainPatch
    {
        //public void FadeIn(float f_fTime = 0.5f, bool f_bUIEnable = false, CameraMain.dgOnCompleteFade f_dg = null, bool f_bSkipable = true, bool f_bColorIsSameOut = true, Color f_color = default(Color))
        [HarmonyPatch(typeof(CameraMain), "FadeIn")]
        [HarmonyPostfix]
        private static void FadeInPost(CameraMain __instance) // string __m_BGMName 못가져옴
        {
            MyLog.LogMessageS("FadeInPost:"+ __instance.GetFadeState());
            //MyLog.Log("OnSelectScenarioPost:" + __m_BGMName);
        }        
        
        [HarmonyPatch(typeof(CameraMain), "FadeOut")]
        [HarmonyPostfix]
        private static void FadeOutPost(CameraMain __instance) // string __m_BGMName 못가져옴
        {
            MyLog.LogMessageS("FadeOutPost:" + __instance.GetFadeState());
            //MyLog.Log("OnSelectScenarioPost:" + __m_BGMName);
        }

        //		CameraMain.FadeOut(float, bool, CameraMain.dgOnCompleteFade, bool, Color) : void @060018DD
    }
}
