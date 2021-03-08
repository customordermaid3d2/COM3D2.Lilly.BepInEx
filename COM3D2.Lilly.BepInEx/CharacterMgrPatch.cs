using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Yotogis;

namespace COM3D2.Lilly.BepInEx
{
    public static class CharacterMgrPatch
    {
        // https://github.com/BepInEx/HarmonyX/wiki/Prefix-changes
        // https://github.com/BepInEx/HarmonyX/wiki/Patch-parameters
        // 
        // 매개 변수의 순서는 중요하지 않지만 이름은 중요합니다. 원본 메소드의 이름과 똑같아야함
        // 모든 매개 변수를 전달할 필요는 없습니다.

        // public CharacterMgr.Preset PresetLoad(BinaryReader brRead, string f_strFileName)

        // 정상
        //[HarmonyPatch(typeof(CharacterMgr), "PresetLoad", new Type[]
        //{
        //            typeof(string)
        //})]
        //[HarmonyPrefix]
        //public static void PresetLoadPrefix(CharacterMgr __instance, string f_strFileName)
        //{
        //    //MyLog.Log("PresetLoadPrefix():" + f_strFileName);
        //}

        // 정상
        //[HarmonyPatch(typeof(CharacterMgr), "PresetLoad", new Type[]
        //{
        //            typeof(BinaryReader) ,typeof(string)
        //})]
        //[HarmonyPostfix]
        //public static void PresetLoadPostfix(CharacterMgr __instance, CharacterMgr.Preset __result, string f_strFileName)
        //{
        //      // __result 조심 
        //    //MyLog.Log("PresetLoadPostfix():" + f_strFileName);
        //}

        // 테스팅 완료
        [HarmonyPatch(typeof(CharacterMgr), "PresetSet")]
        [HarmonyPrefix]
        public static void PresetSetPretfix1(CharacterMgr __instance, Maid f_maid, CharacterMgr.Preset f_prest)
        {
            MyLog.Log("PresetSetPretfix1.f_prest.strFileName:" + f_prest.strFileName);
            MaidProp[] array = getMaidProp(f_prest);
            foreach (MaidProp maidProp in array)
            {
                MyLog.Log("PresetSetPretfix1: " + maidProp.idx + " , " + maidProp.strFileName);
            }
        }

        // public void PresetSet(Maid f_maid, CharacterMgr.Preset f_prest)
        // 테스팅 완료
        [HarmonyPatch(typeof(CharacterMgr), "PresetSet", new Type[]{
            typeof(Maid) ,typeof(CharacterMgr.Preset)
        })]
        [HarmonyPostfix]
        public static void PresetSetPostfix2(Maid f_maid, CharacterMgr.Preset f_prest)
        {
            MyLog.Log("PresetSetPostfix2.f_prest.strFileName:" + f_prest.strFileName);
            MaidProp[] array= getMaidProp(f_prest);
            foreach (MaidProp maidProp in array)
            {
                MyLog.Log("PresetSetPostfix2: " + maidProp.idx + " , " + maidProp.strFileName);
            }
        }

        private static MaidProp[] getMaidProp(CharacterMgr.Preset f_prest)
        {
            MaidProp[] array;
            if (f_prest.ePreType == CharacterMgr.PresetType.Body)
            {
                array = (from mp in f_prest.listMprop
                         where (1 <= mp.idx && mp.idx <= 80) || (115 <= mp.idx && mp.idx <= 122)
                         select mp).ToArray<MaidProp>();
            }
            else if (f_prest.ePreType == CharacterMgr.PresetType.Wear)
            {
                array = (from mp in f_prest.listMprop
                         where 81 <= mp.idx && mp.idx <= 110
                         select mp).ToArray<MaidProp>();
            }
            else
            {
                array = (from mp in f_prest.listMprop
                         where (1 <= mp.idx && mp.idx <= 110) || (115 <= mp.idx && mp.idx <= 122)
                         select mp).ToArray<MaidProp>();
            }

            return array;
        }
    }
}
