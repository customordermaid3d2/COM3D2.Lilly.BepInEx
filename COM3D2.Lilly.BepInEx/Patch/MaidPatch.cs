using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;

namespace COM3D2.Lilly.Plugin
{
    /// <summary>
    /// 메이드 설정 관련
    /// </summary>
    public static class MaidPatch
    {
        // https://github.com/BepInEx/HarmonyX/wiki/Prefix-changes
        // https://github.com/BepInEx/HarmonyX/wiki/Patch-parameters
        // 
        // 매개 변수의 순서는 중요하지 않지만 이름은 중요합니다.
        // 모든 매개 변수를 전달할 필요는 없습니다.

        // public void SetMaterialProperty(MPN m_mpn, TBody.SlotID f_slot, int f_nMatNo, string f_strPropName, string f_strTypeName, string f_strValue)
        [HarmonyPatch(typeof(Maid), "SetMaterialProperty", new Type[]
        { typeof(MPN) ,typeof(TBody.SlotID),typeof(int),typeof(string),typeof(string),typeof(string) })]
        [HarmonyPostfix]
        public static void SetMaterialPropertyPost2(Maid __instance, MPN m_mpn, TBody.SlotID f_slot, int f_nMatNo, string f_strPropName, string f_strTypeName, string f_strValue)
        {
            //if (SceneManager.GetActiveScene().isLoaded)//실패
            if (__instance.Visible)
            {
                MyLog.LogMessageS("Maid.SetMaterialPropertyPost2:" + f_strPropName + "," + f_strTypeName+ "," + f_strValue);
            }

        }


    //------------------------------
    // 정상
    // private void SetProp(MaidProp mp, string filename, int f_nFileNameRID, bool f_bTemp, bool f_bNoScale = false)
    [HarmonyPatch(typeof(Maid), "SetProp", new Type[]
        { typeof(MaidProp) ,typeof(string),typeof(int),typeof(bool),typeof(bool) })]
        [HarmonyPostfix]
        public static void SetPropPost2(Maid __instance, MaidProp mp, string filename, int f_nFileNameRID, bool f_bTemp, bool f_bNoScale)
        {
            if (SceneManager.GetActiveScene().isLoaded)//실패?
            //if( GameMain.Instance.MainCamera.GetFadeState()==CameraMain.FadeState.Out)//효과 없음
            //if (__instance.Visible)
            {
                MyLog.LogMessageS("Maid.SetPropPost2:" + filename + " , " + mp.strFileName + " , " + mp.name);

                //Menu.m_dicResourceRef.ContainsKey(mp.nFileNameRID);
                //Menu.m_dicResourceRef.ge
            }

        }

        // public void SetProp(string tag, string filename, int f_nFileNameRID, bool f_bTemp = false, bool f_bNoScale = false)

        /// <summary>
        /// 메이드에게 메뉴가 설정 되었을시 작동. 뭐지 나중에 오류남
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="tag"></param>
        /// <param name="filename"></param>
        /// <param name="f_nFileNameRID"></param>
        /// <param name="f_bTemp"></param>
        /// <param name="f_bNoScale"></param>
        //[HarmonyPatch(typeof(Maid), "SetProp", new Type[]
        //{ typeof(string) ,typeof(string),typeof(int),typeof(bool),typeof(bool) })]
        //[HarmonyPostfix]
        //public static void SetPropPost0(Maid __instance, string tag, string filename, int f_nFileNameRID, bool f_bTemp , bool f_bNoScale )
        //{
        //    // 로딩중에도 불러와버림. 대책 테스트중
        //    //if (SceneManager.GetActiveScene().isLoaded)//실패
        //    //if (__instance.Visible)            
        //    //{
        //    //    MyLog.Log("Maid.SetPropPre0.filename:" + filename);
        //    //}            
        //}

        //------------------------------
        // private void SetProp(MaidProp mp, string filename, int f_nFileNameRID, bool f_bTemp, bool f_bNoScale = false)
        // 작동 안함
        //[HarmonyPatch(typeof(Maid), "SetProp")]
        //[HarmonyPostfix]
        //public static void SetPropPostfix1(Maid __instance, MaidProp mp, string filename, int f_nFileNameRID, bool f_bTemp, bool f_bNoScale)
        //{
        //
        //}

        //------------------------------
        // public void SetProp(MPN idx, int val, bool f_bTemp = false)
        //[HarmonyPatch(typeof(Maid), "SetProp", new Type[]
        //{
        //            typeof(MPN) ,typeof(int),typeof(bool)
        //})]
        //[HarmonyPostfix]
        //public static void SetPropPostfix3(Maid __instance, MPN idx, int val, bool f_bTemp)
        //{
        //    MyLog.Log("Maid.SetPropPostfix3.strTempFileName:" + __instance.GetProp(idx).strTempFileName);
        //    MyLog.Log("Maid.SetPropPostfix3.strFileName:" + __instance.GetProp(idx).strFileName);
        //}

        //------------------------------
        // public bool DelProp(MPN idx, bool f_bTemp = false)

        [HarmonyPatch(typeof(Maid), "DelProp")]
        [HarmonyPrefix]
        public static void DelPropPrefix4(Maid __instance, MPN idx, bool f_bTemp)
        {
            MyLog.LogMessageS("Maid.DelPropPrefix4");
        }


    }
}
