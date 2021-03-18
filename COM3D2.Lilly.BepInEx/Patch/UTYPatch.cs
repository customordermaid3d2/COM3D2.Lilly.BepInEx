using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin
{
    /// <summary>
    /// UTY
    /// 게임 UI 버튼을 관리
    /// 너무 많이 출력됨
    /// </summary>
    class UTYPatch
    {
        /// <summary>
        /// 너무 많이 출력됨
        /// </summary>
        /// <param name="f_goParent"></param>
        /// <param name="f_strObjName"></param>
        /// <param name="f_bNoError"></param>
        /// <param name="__result"></param>
        // public static GameObject GetChildObject(GameObject f_goParent, string f_strObjName, bool f_bNoError = false)
        //[HarmonyPatch(typeof(UTY), "GetChildObject",new Type[] { typeof(GameObject),typeof(string),typeof(bool) })]
        //[HarmonyPostfix]
        public static void GetChildObject(GameObject f_goParent, string f_strObjName, bool f_bNoError , GameObject __result)
        {
            MyLog.LogMessage("GetChildObject:"+( f_goParent!=null ? f_goParent.name:""), f_strObjName, f_bNoError, __result != null ? __result.name : "");
        }
    }
}
