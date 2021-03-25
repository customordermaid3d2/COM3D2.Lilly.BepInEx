using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin
{
    /// <summary>
    /// FreeModeItemList
    /// FreeModeItemEveryday
    /// FreeModeItemLifeMode
    /// FreeModeItemVip
    /// 에서 사용할 이벤트 목록를 설정
    /// AbstractFreeModeItem 의 값을 수정하면 될듯
    /// </summary>
    class FreeModeItemListPatch
    {
        // private Dictionary<GameObject, AbstractFreeModeItem> item_obj_dic_ = new Dictionary<GameObject, AbstractFreeModeItem>();

        //[HarmonyPatch(typeof(FreeModeItemList), "Awake")]
        //[HarmonyPrefix]//HarmonyPostfix ,HarmonyPrefix
        public void Awake(FreeModeItemList __instance, Dictionary<GameObject, AbstractFreeModeItem> ___item_obj_dic_)
        {
            /*
            this.grid_ = UTY.GetChildObject(base.gameObject, "ListParent/MaskGroup/Contents/Grid", false).GetComponent<UIGrid>();
            this.scroll_view_ = UTY.GetChildObject(base.gameObject, "ListParent/MaskGroup/Contents", false).GetComponent<UIScrollView>();
            this.tab_pabel_ = UTY.GetChildObject(base.gameObject, "ListParent/MaskGroup/Contents/Grid", false).GetComponent<UIWFTabPanel>();
            this.info_label_ = UTY.GetChildObject(base.gameObject, "DetailParent/DetailBlock/Text", false).GetComponent<UILabel>();
            UIGrid component = UTY.GetChildObject(base.gameObject, "DetailParent/ConditionBlock/GridParent", false).GetComponent<UIGrid>();
            List<Transform> childList = component.GetChildList();
            for (int i = 0; i < childList.Count; i++)
            {
                this.condition_label_list_.Add(UTY.GetChildObject(childList[i].gameObject, "Message", false).GetComponent<UILabel>());
            }
            */
        }

        /// <summary>
        ///  프리 모드에서 모든 이벤트 열기 위한용
        /// 사용 실패
        /// </summary>
        /// <param name="item_list"></param>
        // FreeModeItemList
        //[HarmonyPatch(typeof(FreeModeItemList), "SetList")]
        //[HarmonyPrefix]//HarmonyPostfix ,HarmonyPrefix
        public void SetList(AbstractFreeModeItem[] item_list, Dictionary<GameObject, AbstractFreeModeItem> ___item_obj_dic_)
        {   /*
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
            */

            
            foreach (var item in ___item_obj_dic_)
            {
                AbstractFreeModeItem data = item.Value;
                GameObject gameObject= item.Key;

                MyLog.LogMessage("FreeModeItemList"
                    , gameObject.name
                    , data.item_id
                    //, data.isEnabled
                    , data.play_file_name
                    , data.title
                    , data.text
                    , data.type
                    , MyUtill.Join(" | ", data.condition_texts)
                    , data.titleTerm
                    , data.textTerm
                    , MyUtill.Join(" | ", data.condition_text_terms)
                    );

                // 다 활성화 시킬경우 버그 터짐
                /*
                UILabel component = UTY.GetChildObject(gameObject, "Name", false).GetComponent<UILabel>();
                UIWFTabButton component3 = gameObject.GetComponent<UIWFTabButton>();
                component3.isEnabled = true;
                UTY.GetChildObject(gameObject, "HitRect", false).SetActive(true);
                */
            }
        }
    }
}
