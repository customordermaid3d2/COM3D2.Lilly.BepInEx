using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.Patch
{
    /// <summary>
    /// FreeModeItemEveryday
    /// FreeModeItemLifeMode
    /// FreeModeItemVip
    /// 에서 사용할 이벤트 목록를 설정
    /// AbstractFreeModeItem 의 값을 수정하면 될듯
    /// </summary>
    class FreeModeItemListPatch
    {
        /// <summary>
        ///  프리 모드에서 모든 이벤트 열기 위한용
        /// 사용 실패
        /// </summary>
        /// <param name="item_list"></param>
        // FreeModeItemList
        //[HarmonyPatch(typeof(FreeModeItemList), "SetList")]
        //[HarmonyPrefix]//HarmonyPostfix ,HarmonyPrefix
        public void SetList(AbstractFreeModeItem[] item_list)
        {
            foreach (var data in item_list)
            {
                MyLog.LogMessage("SetList 사용가능처리"
                , data.item_id
                , data.is_enabled
                , data.play_file_name
                , data.title
                , data.text
                , data.type
                , MyUtill.Join(" / ", data.condition_texts)
                , data.titleTerm
                , data.textTerm
                , MyUtill.Join(" , ", data.condition_text_terms)
                );
                // 주입이 안된다
                //data.is_enabled = true;
            }
        }
    }
}
