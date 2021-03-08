﻿using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.BepInEx
{
    public static class AudioSourceMgrPatch
    {
        // https://github.com/BepInEx/HarmonyX/wiki/Prefix-changes
        // https://github.com/BepInEx/HarmonyX/wiki/Patch-parameters
        // 
        // 매개 변수의 순서는 중요하지 않지만 이름은 중요합니다.
        // 모든 매개 변수를 전달할 필요는 없습니다.

        // public void LoadPlay(string f_strFileName, float f_fFadeTime, bool f_bStreaming, bool f_bLoop = false)

        // 정상
        [HarmonyPatch(typeof(AudioSourceMgr), "LoadPlay")]
        [HarmonyPrefix]
        public static void LoadPlayPrefix1(string f_strFileName)
        {
            MyLog.Log("LoadPlayPrefix1:" + f_strFileName);
        }

        [HarmonyPatch(typeof(AudioSourceMgr), "LoadPlay")]
        [HarmonyPostfix]
        public static void LoadPlayPostfix2(AudioSourceMgr __instance, string f_strFileName)
        {
            MyLog.Log("LoadPlayPostfix2:" + f_strFileName);
        }
    }
}
