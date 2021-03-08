using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.BepInEx
{
    public static class MaidPatch
    {
        // https://github.com/BepInEx/HarmonyX/wiki/Prefix-changes
        // https://github.com/BepInEx/HarmonyX/wiki/Patch-parameters
        // 
        // 매개 변수의 순서는 중요하지 않지만 이름은 중요합니다.
        // 모든 매개 변수를 전달할 필요는 없습니다.

        // Maid 클래스들의 메소드들은 아예 못들고오는듯

        //------------------------------
        // public void SetProp(string tag, string filename, int f_nFileNameRID, bool f_bTemp = false, bool f_bNoScale = false)
        [HarmonyPatch(typeof(Maid), "SetProp", new Type[]
        {
                    typeof(string) ,typeof(string),typeof(int),typeof(bool),typeof(bool)
        })]
        [HarmonyPostfix]
        public static void SetPropPostfix0(Maid __instance, string tag, string filename, int f_nFileNameRID, bool f_bTemp , bool f_bNoScale )
        {
            MyLog.Log("Maid.SetPropPrefix0.filename:" + filename);
        }

        //------------------------------
        // private void SetProp(MaidProp mp, string filename, int f_nFileNameRID, bool f_bTemp, bool f_bNoScale = false)
        // 작동 안함
        [HarmonyPatch(typeof(Maid), "SetProp")]
        [HarmonyPostfix]
        public static void SetPropPostfix1(Maid __instance, MaidProp mp, string filename, int f_nFileNameRID, bool f_bTemp, bool f_bNoScale)
        {
            MyLog.Log("Maid.SetPropPrefix1.mp.strTempFileName:" + mp.strTempFileName);
            MyLog.Log("Maid.SetPropPrefix1.filename:" + filename);
        }

        //------------------------------
        // public void SetProp(MPN idx, int val, bool f_bTemp = false)
        [HarmonyPatch(typeof(Maid), "SetProp", new Type[]
        {
                    typeof(MPN) ,typeof(int),typeof(bool)
        })]
        [HarmonyPostfix]
        public static void SetPropPostfix3(Maid __instance, MPN idx, int val, bool f_bTemp)
        {
            MyLog.Log("Maid.SetPropPostfix3.strTempFileName:" + __instance.GetProp(idx).strTempFileName);
            MyLog.Log("Maid.SetPropPostfix3.strFileName:" + __instance.GetProp(idx).strFileName);
        }

        //------------------------------
        // public bool DelProp(MPN idx, bool f_bTemp = false)

        [HarmonyPatch(typeof(Maid), "DelProp")]
        [HarmonyPrefix]
        public static void DelPropPrefix4(Maid __instance, MPN idx, bool f_bTemp)
        {
            MyLog.Log("Maid.DelPropPrefix4");
        }


    }
}
