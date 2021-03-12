﻿using HarmonyLib;
using System;
using System.Collections.Generic;

namespace COM3D2.Lilly.Plugin
{
    internal class ScenarioDataPatch 
    {
        // public void ScenarioPlay(List<Maid> maid_list)
        // AddEventMaid
        [HarmonyPatch(typeof(ScenarioData), "ScenarioPlay")]
        [HarmonyPostfix]
        private static void ScenarioPlay()
        {
            // !maid.status.GetEventEndFlag(this.ID)
            MyLog.LogMessageS("ScenarioPlay");
           //for (int j = 0; j < GameMain.Instance.CharacterMgr.GetStockMaidCount(); j++)
           //{
           //    Maid stockMaid = GameMain.Instance.CharacterMgr.GetStockMaid(j);
           //
           //}
        }

        // 이게 이벤트 처리 같음
        // public void RemoveEventMaid(Maid maid, bool not_again = false)
        [HarmonyPatch(typeof(ScenarioData), "RemoveEventMaid")]
        [HarmonyPostfix]
        private static void RemoveEventMaid(Maid maid, bool not_again, List<Maid> ___m_EventMaid, int ___ID)
        {
            // !maid.status.GetEventEndFlag(this.ID)
            MyLog.LogMessageS("RemoveEventMaid");
            //for (int j = 0; j < GameMain.Instance.CharacterMgr.GetStockMaidCount(); j++)
            //{
            //    Maid stockMaid = GameMain.Instance.CharacterMgr.GetStockMaid(j);
            //
            //}

            return;
            if (___m_EventMaid.Contains(maid))
            {
                ___m_EventMaid.Remove(maid);
                if (not_again)
                {
                    maid.status.SetEventEndFlag(___ID, true);
                }
            }
        }

        //public bool CheckPlayableCondition(ScenarioData.PlayableCondition condition, bool maid_update = true)
        [HarmonyPatch(typeof(ScenarioData), "CheckPlayableCondition", new Type[] { typeof(ScenarioData.PlayableCondition), typeof(bool) })]
        [HarmonyPostfix]
        private static void CheckPlayableCondition1(
            ScenarioData.PlayableCondition condition            , 
            bool maid_update ,
            List<Maid> ___m_EventMaid,
            Dictionary<ScenarioData.PlayableCondition, ScenarioData.PlayableData> ___m_PlayableData,
            ScenarioData __instance
            )
        {
            // !maid.status.GetEventEndFlag(this.ID)
            MyLog.LogMessageS("CheckPlayableCondition1" + condition);
            // CheckPlayableCondition1シナリオ
            //for (int j = 0; j < GameMain.Instance.CharacterMgr.GetStockMaidCount(); j++)
            //{
            //    Maid stockMaid = GameMain.Instance.CharacterMgr.GetStockMaid(j);
            //
            //}
            //ScenarioData.PlayableData playableData = this.m_PlayableData[condition];
            //CharacterMgr characterMgr = GameMain.Instance.CharacterMgr;
            //FacilityManager facilityMgr = GameMain.Instance.FacilityMgr;

            // 이벤트 했다는 플레그 설정
            ScenarioDataUtill.SetEventEndFlagAll(___m_EventMaid, __instance);

            //MyLog.LogMessageS("CheckPlayableCondition1.___m_EventMaid");
            //foreach (var item in ___m_PlayableData)
            //{
            //    MyLog.LogMessageS(".___m_PlayableData:" + item.Key);
            //    foreach (var item1 in item.Value.StrdataArray)
            //    {
            //        MyLog.LogMessageS("..StrdataArray:" + item1);
            //    }
            //}
        }


        // 의미 없음?
        // private bool CheckPlayableCondition(bool update_important = false)
        [HarmonyPatch(typeof(ScenarioData), "CheckPlayableCondition",new Type[] {typeof(bool) })]
        [HarmonyPostfix]
        private static void CheckPlayableCondition2(bool update_important )
        {
            // !maid.status.GetEventEndFlag(this.ID)
            //MyLog.LogMessageS("CheckPlayableCondition2");
           //for (int j = 0; j < GameMain.Instance.CharacterMgr.GetStockMaidCount(); j++)
           //{
           //    Maid stockMaid = GameMain.Instance.CharacterMgr.GetStockMaid(j);
           //
           //}
        }
    }
}