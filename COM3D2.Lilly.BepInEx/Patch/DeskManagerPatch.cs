using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin
{
    class DeskManagerPatch
    {
        DeskManagerPatch()
        {
            // Traverse.CreateWithType("DeskManager");
        }
        //public static void OnChangeBG(string bg_name, GameObject bg_object)
        // [Error  :  HarmonyX] Failed to process patch System.Void COM3D2.Lilly.Plugin.BgMgrPatch.OnChangeBGPost(System.String, UnityEngine.GameObject) - Could not find method OnChangeBG with NULL parameters in type BgMgr
        // [HarmonyPatch(typeof(DeskManager), "OnChangeBG")]// 보호수준때문에 사용 불가 internal class DeskManager
        //[HarmonyPatch("OnChangeBG")]// 보호수준때문에 사용 불가 internal class DeskManager
        //[HarmonyPostfix]
        private static void OnChangeBGPost(string bg_name, GameObject bg_object)
        {
            MyLog.LogMessage("BgMgr.OnChangeBGPost:" + bg_name);
        }
         
    }
}
