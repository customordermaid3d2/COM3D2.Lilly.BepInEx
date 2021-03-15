using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wf;

namespace COM3D2.Lilly.Plugin
{
    // CsvCommonIdManager

    class CsvCommonIdManagerPatch
    {
        /// <summary>
        /// 안됨
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="csvTopCommonName"></param>
        /// <param name="typeName"></param>
        /// <param name="type"></param>
        /// <param name="customCheckFunction"></param>
        // public CsvCommonIdManager(string csvTopCommonName, string typeName, CsvCommonIdManager.Type type, Func<int, bool> customCheckFunction = null)
        [HarmonyPatch(typeof(CsvCommonIdManager),new Type[] { typeof(string) , typeof(string)  , typeof(CsvCommonIdManager)  , typeof(Func<int, bool>) })]
        //[HarmonyPostfix]
        private static void ctor(CsvCommonIdManagerPatch __instance, string csvTopCommonName, string typeName, CsvCommonIdManager.Type type, Func<int, bool> customCheckFunction ) // string __m_BGMName 못가져옴
        {
            MyLog.LogMessage("CsvCommonIdManager:" + csvTopCommonName +" , "+ typeName + " , " + type + " , " + (customCheckFunction==null));
        }
    }
}
