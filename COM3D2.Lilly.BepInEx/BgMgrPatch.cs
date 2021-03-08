using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    class BgMgrPatch
    {
        // public void ChangeBg(string f_strPrefubName)
        /// <summary>
        /// 배경 변경시?
        /// </summary>
        /// <param name="f_strPrefubName"></param>
        [HarmonyPatch(typeof(BgMgr), "ChangeBg")]
        [HarmonyPostfix]
        private static void ChangeBgPost(string f_strPrefubName)
        {
            MyLog.Log("ChangeBgPost:" + f_strPrefubName);
        }
    }
}
