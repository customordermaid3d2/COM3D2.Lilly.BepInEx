using HarmonyLib;
using MaidStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    /// <summary>
    ///  프리 모드에서 모든 이벤트 열기 위한용
    ///  분석용
    ///  일상 이벤트
    /// </summary>
    /// TypeDefinition type10 = CS$<>8__locals1.ass.MainModule.GetType("FreeModeItemEveryday");
    class FreeModeItemEverydayPatch
    {
        // FreeModeItemEveryday

        //type10.GetMethod("get_is_enabled").InjectWith(method2, 0, null, InjectFlags.ModifyReturn, InjectDirection.Before, null, null);


        //type10.GetMethod("IsEnabledFlag").InjectWith(method2, 0, null, InjectFlags.ModifyReturn, InjectDirection.Before, null, null);
        //public static bool IsEnabledFlag(FreeModeItemEveryday.ScnearioType type, string flag_name)
        /// <summary>
        /// AbstractFreeModeItemPatch와 중복됨
        /// 버그남
        /// </summary>
        /// <param name="__result"></param>
        /// <param name="type"></param>
        /// <param name="flag_name"></param>
        [HarmonyPatch(typeof(FreeModeItemEveryday), "IsEnabledFlag")]
        [HarmonyPrefix]
        [HarmonyPostfix]
        public static bool IsEnabledFlag(bool __result)
        {
            __result = true;
            AbstractFreeModeItemPatch.OutMsg("FreeModeItemEveryday.IsEnabledFlag");
            return true;
           // return false;
        }

        [HarmonyPatch(typeof(FreeModeItemEveryday), "is_enabled", MethodType.Getter)]
        [HarmonyPrefix]//HarmonyPostfix ,HarmonyPrefix
        [HarmonyPostfix]
        public static bool get_is_enabled( bool __result)
        {
            __result = true;
            AbstractFreeModeItemPatch.OutMsg("FreeModeItemEveryday.get_is_enabled");
            return true;
            //return false;
        }


        /*
        public static Dictionary<FreeModeItemEveryday.ScnearioType, FreeModeItemEveryday.ScnerioData> DataDic;        
        
        [HarmonyPatch(typeof(FreeModeItemEveryday), MethodType.Constructor)]
        [HarmonyPostfix]
        public static void FreeModeItemEveryday(
            Dictionary<FreeModeItemEveryday.ScnearioType, FreeModeItemEveryday.ScnerioData> ___DataDic
        )
        {
            DataDic = ___DataDic;
        }
        */

        //[HarmonyPatch(typeof(FreeModeItemEveryday), "CreateItemEverydayList",new Type[] { typeof(FreeModeItemEveryday.ScnearioType), typeof(Status) })]
        //[HarmonyPostfix]
        public static void CreateItemEverydayList(List<FreeModeItemEveryday> __result, FreeModeItemEveryday.ScnearioType type, Status maidStatus )
        {

        }

        /*

        public class ScnerioData
        {
            // Token: 0x06003655 RID: 13909 RVA: 0x0019B50E File Offset: 0x0019990E
            public ScnerioData(string FixingFlagText)
            {
                this.FixingFlagText = FixingFlagText;
            }

            // Token: 0x0400309D RID: 12445
            public SortedDictionary<int, EverydayEventData> everyday_event_data_dic_;
            //public SortedDictionary<int, FreeModeItemEveryday.EverydayEventData> everyday_event_data_dic_;

            // Token: 0x0400309E RID: 12446
            public HashSet<string> flag_name_set_ = new HashSet<string>();

            // Token: 0x0400309F RID: 12447
            public string FixingFlagText = "シーン鑑賞_一般_フラグ_";
        }

        public class EverydayEventData
        {
            // Token: 0x040030A0 RID: 12448
            public AbstractFreeModeItem.GameMode gameMode;

            // Token: 0x040030A1 RID: 12449
            public int id;

            // Token: 0x040030A2 RID: 12450
            public string name;

            // Token: 0x040030A3 RID: 12451
            public string call_file_name;

            // Token: 0x040030A4 RID: 12452
            public string check_flag_name;

            // Token: 0x040030A5 RID: 12453
            public string info_text;

            // Token: 0x040030A6 RID: 12454
            public bool netorare;

            // Token: 0x040030A7 RID: 12455
            public int subHerionID;

            // Token: 0x040030A8 RID: 12456
            public string[] condition_texts;

            // Token: 0x040030A9 RID: 12457
            public List<int> personalIdList = new List<int>();
        }

        */

    }


}
