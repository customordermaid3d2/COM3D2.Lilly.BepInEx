using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using wf;

namespace COM3D2.Lilly.Plugin
{
    /// <summary>
    /// 이벤트 설정 화면
    /// </summary>
    public static class SceneScenarioSelectPatch
    {
        // 이걸론 안됨
        /// <summary>
        /// 이벤트 마다 함수 호출됨
        /// </summary>
        /// <param name="___m_CurrentScenario"></param>
        //[HarmonyPatch(typeof(SceneScenarioSelect), "OnSelectScenario")]
        //[HarmonyPostfix]
        private static void OnSelectScenarioPost(ScenarioData ___m_CurrentScenario) // string __m_BGMName 못가져옴
        {
            //쓰지 말자 거의 도움 안됨
            MyLog.LogMessageS("OnSelectScenarioPost:" + ___m_CurrentScenario.Title + " , " + ___m_CurrentScenario.EventContents);

        }

        // 이걸론 안됨
        //[HarmonyPatch(typeof(SceneScenarioSelect), "Start")]
        //[HarmonyPostfix]
        private static void StartPost(UILabel ___m_ContentsLabel, List<Maid> ___m_SelectedMaid, List<UILabel> ___m_PlayableTextUIList) // string __m_BGMName 못가져옴
        {
            MyLog.LogMessageS("StartPost");
            //foreach (var item in ___m_PlayableTextUIList)
            //{
            //    MyLog.LogMessageS("StartPost:" + item.text );
            //}

        }

        /// <summary>
        /// 이게 스크립트 불러와서 실행 처리
        /// </summary>
        [HarmonyPatch(typeof(SceneScenarioSelect), "JumpNextLabel")]
        [HarmonyPostfix]
        private static void JumpNextLabel(ScenarioData ___m_CurrentScenario, List<Maid> ___m_SelectedMaid) 
        {
            
            //if (this.m_JumpLabel == this.m_OkLabel)
            {
                //this.m_CurrentScenario.ScenarioPlay(this.m_SelectedMaid);
                //NDebug.Assert(!string.IsNullOrEmpty(this.m_CurrentScenario.ScenarioScript), "SceneScenarioSelect.cs:選択したシナリオのスクリプトファイル名が設定されてません");
                //NDebug.Assert(!string.IsNullOrEmpty(this.m_CurrentScenario.ScriptLabel), "SceneScenarioSelect.cs:選択したシナリオのラベル名が設定されてません");
                string text = ___m_CurrentScenario.ScenarioScript;
                MyLog.LogMessageS("JumpNextLabel: "+text);
                if (___m_SelectedMaid.Count > 0 && text.IndexOf("?") >= 0)
                {
                    text = ScriptManager.ReplacePersonal(___m_SelectedMaid[0], text);
                    MyLog.LogMessageS("JumpNextLabel: "+text);
                }
                // 스크립트 실행 부분
                //GameMain.Instance.ScriptMgr.EvalScript("&tf['scenario_file_name'] = '" + text + "';");
                //GameMain.Instance.ScriptMgr.EvalScript("&tf['label_name'] = '" + this.m_CurrentScenario.ScriptLabel + "';");
            }
            //this.m_AdvkagMgr.JumpLabel(this.m_JumpLabel);
            //this.m_AdvkagMgr.Exec();
        }




        
        // 이걸론 안됨
        //[HarmonyPatch(typeof(SceneScenarioSelect), "SetScenarioPlate")]
        //[HarmonyPostfix]
        private static void SetScenarioPlate(
            Dictionary<UIWFTabButton, ScenarioData> ___m_ScenarioButtonpair,
            List<Maid> ___m_SelectedMaid,
            UIWFTabPanel ___m_UIWFTabPanel,
            //SceneScenarioSelect.NeedScrollUI ___m_ScenarioScroll,
            UIButton ___m_OkButton
            )
        {
            //MyLog.LogMessageS("SetScenarioPlate.GetAllScenarioData");
            //foreach (ScenarioData scenarioData in GameMain.Instance.ScenarioSelectMgr.GetAllScenarioData())
            //{
            //    //if (scenarioData.IsPlayable)
            //    {
            //        MyLog.LogMessageS(".GetAllScenarioData:" + scenarioData.Title);// +","+ scenarioData.ConditionText//함수
            //        foreach (string str in scenarioData.ConditionText)
            //        {
            //            MyLog.LogMessageS(".ConditionText:" + str);// 조건
            //        }
            //    }
            //}

            //MyLog.LogMessageS("SetScenarioPlate.___m_ScenarioButtonpair");
            //foreach (var item in ___m_ScenarioButtonpair)
            //{
            //    MyLog.LogMessageS(".___m_ScenarioButtonpair:" + item.Value.Title + "," + item.Value.EventContents+ "," + item.Value.ScriptLabel+ "," + item.Value.Notification);
            //    foreach (string str in item.Value.ConditionText)
            //    {
            //        MyLog.LogMessageS(".ConditionText:" + str);// 조건
            //    }
            //}

           // MyLog.LogMessageS("SetScenarioPlate.___m_SelectedMaid");
            //foreach (var item in ___m_SelectedMaid)
            //{
            //    MyLog.LogMessageS(".___m_SelectedMaid:" +item.status.firstName + " , " + item.status.lastName);
            //}

                // 원본 코드
                return;
            /*
            foreach (ScenarioData scenarioData in GameMain.Instance.ScenarioSelectMgr.GetAllScenarioData())
            {
                if (scenarioData.IsPlayable)
                {
                    GameObject gameObject = null;//Utility.CreatePrefab(___m_ScenarioScroll.Grid.gameObject, "SceneScenarioSelect/Prefab/ScenarioPlate", true);
                    UILabel component = UTY.GetChildObject(gameObject, "Title", false).GetComponent<UILabel>();
                    component.text = scenarioData.Title;
                    Utility.SetLocalizeTerm(component, scenarioData.TitleTerm, false);
                    gameObject.name = scenarioData.NotLineTitle;
                    UTY.GetChildObject(gameObject, "Icon", false).GetComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>("SceneScenarioSelect/Sprite/" + scenarioData.IconName);
                    UIWFTabButton componentInChildren = gameObject.GetComponentInChildren<UIWFTabButton>();
                    //EventDelegate.Add(componentInChildren.onSelect, new EventDelegate.Callback(___OnSelectScenario));
                    ___m_ScenarioButtonpair.Add(componentInChildren, scenarioData);
                }
            }
            */
            //___m_ScenarioScroll.Grid.repositionNow = true;
            //___m_ScenarioScroll.ScrollView.ResetPosition();
            /*
            ___m_UIWFTabPanel.UpdateChildren();
            if (___m_ScenarioButtonpair.Count<KeyValuePair<UIWFTabButton, ScenarioData>>() > 0)
            {
                ___m_UIWFTabPanel.Select(___m_ScenarioButtonpair.Keys.First<UIWFTabButton>());
            }
            else
            {
                ___m_OkButton.isEnabled = false;
            }
            */
        }


        // 이걸론 안됨
        //[HarmonyPatch(typeof(SceneScenarioSelect), "SetPlayableText")]
        //[HarmonyPostfix]
        private static void SetPlayableText(UILabel ___m_ContentsLabel, List<Maid> ___m_SelectedMaid, List<UILabel> ___m_PlayableTextUIList) // string __m_BGMName 못가져옴
        {
            MyLog.LogMessageS("SetPlayableText");
            //foreach (var item in ___m_PlayableTextUIList)
            //{
            //    MyLog.LogMessageS("SetPlayableText:" + item.text);
            //}

        }

        // 이걸론 안됨
        [HarmonyPatch(typeof(SceneScenarioSelect), "UpdateCharaUI")]
        [HarmonyPostfix]
        private static void UpdateCharaUI(
            UILabel ___m_ContentsLabel,
            List<Maid> ___m_SelectedMaid,
            List<UILabel> ___m_PlayableTextUIList,
            ScenarioData ___m_CurrentScenario,
            CharacterSelectManager ___m_CharaSelectMgr) // string __m_BGMName 못가져옴
        {
            MyLog.LogMessageS("UpdateCharaUI:" + ___m_CurrentScenario.ScenarioScript +" , " + ___m_CurrentScenario.ScriptLabel +" , " + ___m_CurrentScenario.Title + " , " + ___m_CurrentScenario.EventContents);            
            //MyLog.LogMessageS("UpdateCharaUI:" + ___m_CurrentScenario.EventContents);
            //MyLog.LogMessageS("UpdateCharaUI:" + ___m_CurrentScenario.Title);
            //MyLog.LogMessageS("UpdateCharaUI:" + ___m_CurrentScenario.ConditionText);            
            //MyLog.LogMessageS("UpdateCharaUI:" + ___m_CurrentScenario.ConditionTextTerms);
            //MyLog.LogMessageS("UpdateCharaUI:" + ___m_CurrentScenario.Notification);
            foreach (var item in ___m_CurrentScenario.ConditionText)
            {
                MyLog.LogMessageS("UpdateCharaUI:" + item);
            }

            // SceneScenarioSelect
            // Token: 0x06003F9A RID: 16282 RVA: 0x001F3004 File Offset: 0x001F1404
            
            //CharacterSelectManager m_CharaSelectMgr=new CharacterSelectManager();
            ////this.m_SelectedMaid.Clear();
            //if (___m_CurrentScenario.IsFixedMaid)
            //{
            //    //___m_CharaSelectMgr.Create(CharacterSelectManager.Type.Multiple, ___m_CurrentScenario.EventMaidCount, true);
            //    //for (int i = 0; i < ___m_CurrentScenario.EventMaidCount; i++)
            //    //{
            //    //    ___m_SelectedMaid.Add(___m_CurrentScenario.GetEventMaid(i));
            //    //}
            //    foreach (var maid in ___m_SelectedMaid)
            //    {
            //        MyLog.LogMessageS("UpdateCharaUI1:" + MaidUtill.GetMaidFullNale(maid));
            //    }
            //    //base.StartCoroutine(___ColiderOff());
            //}
            //else
            //{
            //    CharacterSelectManager.CallBackOnSelect callBackCallBackOnSelect = delegate (Maid maid)
            //    {
            //        MyLog.LogMessageS("UpdateCharaUI2:" + MaidUtill.GetMaidFullNale(maid));
            //        //___m_SelectedMaid.Clear();
            //        //___m_SelectedMaid.Add(maid);
            //    };
            //    m_CharaSelectMgr.SetCallBackCallBackOnSelect(callBackCallBackOnSelect);
            //    m_CharaSelectMgr.Create(CharacterSelectManager.Type.Select, 3, true);
            //}
            //m_CharaSelectMgr.big_thumbnail.Visible = false;
            //for (int j = 0; j < GameMain.Instance.CharacterMgr.GetStockMaidCount(); j++)
            //{
            //    Maid stockMaid = GameMain.Instance.CharacterMgr.GetStockMaid(j);
            //    if (!___m_CurrentScenario.ExistEventMaid(stockMaid))
            //    {
            //        m_CharaSelectMgr.RemoveMaidPlate(stockMaid);
            //    }
            //}
            //if (___m_CurrentScenario.IsFixedMaid || ___m_CurrentScenario.EventMaidCount == 0)
            //{
            //    return;
            //}
            //if (___m_CurrentScenario.EventMaidCount != GameMain.Instance.CharacterMgr.GetStockMaidCount())
            //{
            //    List<Transform> childList = m_CharaSelectMgr.MaidPlateParentGrid.GetChildList();
            //    m_CharaSelectMgr.SelectMaid(childList[0].GetComponent<MaidPlate>().maid);
            //}
        }
        /*

        */
    }
}
