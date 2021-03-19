using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wf;

namespace COM3D2.Lilly.Plugin
{
    class EmpireLifeModeDataPatch
    {
        // EmpireLifeModeData
        public static CsvCommonIdManager commonIdManager;
        public static Dictionary<int, EmpireLifeModeData.Data> basicDatas;

        [HarmonyPatch(typeof(EmpireLifeModeData), "CreateData")]
        [HarmonyPostfix]//HarmonyPostfix ,HarmonyPrefix
        public static void CreateData(CsvCommonIdManager ___commonIdManager, Dictionary<int, EmpireLifeModeData.Data> ___basicDatas)//EmpireLifeModeData __instance,
        {
            commonIdManager = ___commonIdManager;
            basicDatas = ___basicDatas;
            //foreach (EmpireLifeModeData.Data item in ___basicDatas.Values)
            //{
            //    //item.dataFlagMaid
            //}
        }
    }
}
