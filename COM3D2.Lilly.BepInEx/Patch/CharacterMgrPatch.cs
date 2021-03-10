﻿using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Yotogis;

namespace COM3D2.Lilly.Plugin
{
    /// <summary>
    /// 캐릭터 설정 관련
    /// </summary>
    public static class CharacterMgrPatch // 이름은 마음대로 지어도 되긴 한데 나같은 경우 정리를 위해서 해킹 대상 클래스 이름에다가 접미사를 붇임
    {
        // https://github.com/BepInEx/HarmonyX/wiki/Prefix-changes
        // https://github.com/BepInEx/HarmonyX/wiki/Patch-parameters
        // 
        // 매개 변수의 순서는 중요하지 않지만 이름은 중요합니다. 원본 메소드의 이름과 똑같아야함
        // 모든 매개 변수를 전달할 필요는 없습니다.

        // ------------------------------------

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
        /// <summary>
        /// 주의. 에딧 모드에서 프리셋 창 뜰때 이 함수를 이용해서 파일들을 다 읽어옴
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="__result"></param>
        /// <param name="f_strFileName"></param>
        [HarmonyPatch(typeof(CharacterMgr), "PresetLoad", new Type[]
        {
                    typeof(BinaryReader) ,typeof(string)
        })]
        [HarmonyPostfix]
        public static void PresetLoadPostfix(CharacterMgr __instance, string f_strFileName)
        {
            //MyLog.Log("PresetLoadPostfix():" + f_strFileName);
        }

        // ------------------------------------

        /// <summary>
        /// 프리셋 선택해서 메이드에게 입힐때 작동
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="f_maid"></param>
        /// <param name="f_prest"></param>
        [HarmonyPatch(typeof(CharacterMgr), "PresetSet")]
        [HarmonyPrefix]
        public static void PresetSetPretfix1(CharacterMgr __instance, Maid f_maid, CharacterMgr.Preset f_prest)
        {
            MyLog.LogMessageS("PresetSetPretfix1.f_prest.strFileName:" + f_prest.strFileName);
            MaidProp[] array = getMaidProp(f_prest);
            foreach (MaidProp maidProp in array)
            {
                MyLog.LogMessageS("PresetSetPretfix1: " + maidProp.idx + " , " + maidProp.strFileName);
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
            MyLog.LogMessageS("PresetSetPostfix2.f_prest.strFileName:" + f_prest.strFileName);
            MaidProp[] array= getMaidProp(f_prest);
            foreach (MaidProp maidProp in array)
            {
                MyLog.LogMessageS("PresetSetPostfix2: " + maidProp.idx + " , " + maidProp.strFileName);
            }
        }

        /// <summary>
        /// 프리셋에서 불러온 메뉴들을 반납
        /// </summary>
        /// <param name="f_prest"></param>
        /// <returns></returns>
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

        // ------------------------------------

        // public void SetActiveMaid(Maid f_maid, int f_nActiveSlotNo)

        [HarmonyPatch(typeof(CharacterMgr), "SetActiveMaid", new Type[]{
            typeof(Maid) ,typeof(int)
        })]
        [HarmonyPostfix]
        public static void SetActiveMaidPost0(Maid f_maid, int f_nActiveSlotNo)
        {
            MyLog.LogMessageS("CharacterMgr.SetActiveMaidPost0: " + f_maid.status.firstName + " , "+ f_maid.status.lastName);
        }

        // public CharacterMgr.Preset PresetSave(Maid f_maid, CharacterMgr.PresetType f_type)

        [HarmonyPatch(typeof(CharacterMgr), "PresetSave")]
        [HarmonyPostfix]
        public static void PresetSavePost0(Maid f_maid, CharacterMgr.PresetType f_type, CharacterMgr.Preset __result)
        {
            MyLog.LogMessageS("CharacterMgr.PresetSavePost0: " + f_maid.status.firstName + " , " + f_maid.status.lastName +" , "+ __result.strFileName);
        }
    }
}