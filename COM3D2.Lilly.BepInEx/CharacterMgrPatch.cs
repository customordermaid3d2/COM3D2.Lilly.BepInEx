using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Yotogis;

namespace COM3D2.Lilly.BepInEx
{
    public static class CharacterMgrPatch
    {
        // https://github.com/BepInEx/HarmonyX/wiki/Prefix-changes
        // https://github.com/BepInEx/HarmonyX/wiki/Patch-parameters
        // 
        // 매개 변수의 순서는 중요하지 않지만 이름은 중요합니다.
        // 모든 매개 변수를 전달할 필요는 없습니다.

        // public CharacterMgr.Preset PresetLoad(BinaryReader brRead, string f_strFileName)

        [HarmonyPatch(typeof(CharacterMgr), "PresetLoad", new Type[]
        {
                    typeof(string)
        })]
        [HarmonyPrefix]
        public static void PresetLoadPrefix(CharacterMgr __instance, string f_strFileName)
        {
            MyLog.Log("PresetLoadPrefix():" + f_strFileName);
        }

        [HarmonyPatch(typeof(CharacterMgr), "PresetLoad", new Type[]
        {
                    typeof(BinaryReader) ,typeof(string)
        })]
        [HarmonyPostfix]
        public static void PresetLoadPostfix(CharacterMgr __instance, string f_strFileName)
        {
            MyLog.Log("PresetLoadPostfix():" + f_strFileName);
        }

        [HarmonyPatch(typeof(CharacterMgr), "PresetSet")]
        [HarmonyPostfix]
        public static void PresetSetPostfix(CharacterMgr __instance, CharacterMgr.Preset f_prest)
        {
            MyLog.Log("PresetSetPostfix():" + f_prest.strFileName);
        }


    }
}
