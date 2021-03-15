using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    class FreeModeItemEverydayPatch
    {
        //public static Dictionary<FreeModeItemEveryday.ScnearioType, FreeModeItemEveryday.ScnerioData> DataDic;// 보호 수준 젠장
        public static List<int> disableIdList = new List<int>();

        // FreeModeItemEveryday

        //[HarmonyPatch(typeof(FreeModeItemEveryday), "CreateCsvData")]
        //[HarmonyPostfix]
        private static void CreateCsvData(FreeModeItemEveryday.ScnearioType type)
        {

        }

    }
}
