using MaidStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM3D2.Lilly.Plugin
{
    /// <summary>
    /// 이벤트 관련
    /// </summary>
    public static class ScenarioDataUtill
    {
        static CharacterMgr characterMgr = GameMain.Instance.CharacterMgr;
        static ﻿ScriptManager﻿ scriptManager = new ScriptManager();

        /// <summary>
        /// 모든 이벤트 처리용
        /// </summary>
        public  static void SetScenarioDataAll()
        {
            MyLog.LogDebugS("ScenarioDataUtill.SetScenarioDataAll");

            // 병렬 처리
            Parallel.ForEach(GameMain.Instance.ScenarioSelectMgr.GetAllScenarioData(), scenarioData =>
            {
                // MyLog.LogMessageS(".SetScenarioDataAll:" + scenarioData.ID + " , " + scenarioData.ScenarioScript + " , " + scenarioData.IsPlayable + " , " + scenarioData.Title); ;
                if (scenarioData.IsPlayable)
                {                    
                    SetEventEndFlagAll(scenarioData.GetEventMaidList(), scenarioData);
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="___m_EventMaid"></param>
        /// <param name="scenarioData"></param>
        public static void SetEventEndFlagAll(List<Maid> ___m_EventMaid, ScenarioData scenarioData)
        {
            Action<Maid> action = delegate (Maid maid)
            {
                // //___select_maid_.status.specialRelation = SpecialRelation.Married;// 호감도
                //maid.status.RemoveEventEndFlagAll();

                
                maid.status.SetEventEndFlag(scenarioData.ID, true);
                if (scenarioData.ScenarioScript.Contains("_Marriage"))
                {
                    maid.status.specialRelation = SpecialRelation.Married;
                    maid.status.relation = Relation.Lover;
                    maid.status.OldStatus.isMarriage = true;
                    maid.status.OldStatus.isNewWife = true;
                    //SetMaidCondition(0, '嫁');
                }

                //scriptManager.EvalScript("&tf['scenario_file_name'] = '" + __instance.ScenarioScript + "';");
                //scriptManager.EvalScript("&tf['label_name'] = '" + __instance.ScriptLabel + "';");

            };
            bool b;
            //MyLog.LogMessageS(".m_EventMaid");
            foreach (var maid in ___m_EventMaid)
            {
                b = maid.status.GetEventEndFlag(scenarioData.ID);
                MyLog.LogMessageS(".SetEventEndFlagAll:" + scenarioData.ID +" , " + scenarioData.ScenarioScript + " , " + maid.status.firstName + " , " + maid.status.lastName + " , " + b + " , " + scenarioData.Title); ;
                if (!b)
                {
                    action(maid);
                }
            }
        }

        /// <summary>
        ///  CheckPlayableCondition 참고
        /// </summary>
        public static void RemoveEventEndFlagAll()
        {
            
            Action<Maid> action = delegate (Maid maid)
            {
                    maid.status.RemoveEventEndFlagAll();             
            };
            for (int j = 0; j < characterMgr.GetStockMaidCount(); j++)
            {
                Maid stockMaid = characterMgr.GetStockMaid(j);
                MyLog.LogMessageS(".RemoveEventEndFlagAll:" + stockMaid.status.firstName + " , " + stockMaid.status.lastName ); ;
                action(stockMaid);
            }

        }

        //public void RemoveEventMaid(Maid maid, bool not_again = false)
        //{
        //    if (this.m_EventMaid.Contains(maid))
        //    {
        //        this.m_EventMaid.Remove(maid);
        //        if (not_again)
        //        {
        //            maid.status.SetEventEndFlag(this.ID, true);
        //        }
        //    }
        //}

    }
}
