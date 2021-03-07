using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.BepInEx
{
    class AudioSourceMgrPatch
    {
        // https://github.com/BepInEx/HarmonyX/wiki/Prefix-changes
        // https://github.com/BepInEx/HarmonyX/wiki/Patch-parameters
        // 
        // 매개 변수의 순서는 중요하지 않지만 이름은 중요합니다.
        // 모든 매개 변수를 전달할 필요는 없습니다.

        // public void LoadPlay(string f_strFileName, float f_fFadeTime, bool f_bStreaming, bool f_bLoop = false)

        [HarmonyPatch(typeof(AudioSourceMgr), "LoadPlay")]
        [HarmonyPrefix]
        public static void LoadPlayPrefix(CharacterMgr __instance, string f_strFileName)
        {
            MyLog.Log("LoadPlayPrefix():" + f_strFileName);
        }

        [HarmonyPatch(typeof(AudioSourceMgr), "LoadPlay")]
        [HarmonyPostfix]
        public static void LoadPlayPostfix(CharacterMgr __instance, string f_strFileName)
        {
            MyLog.Log("LoadPlayPostfix():" + f_strFileName);
        }
    }
}
