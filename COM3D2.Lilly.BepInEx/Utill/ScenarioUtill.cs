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
    public static class ScenarioUtill
    {
        static CharacterMgr characterMgr = GameMain.Instance.CharacterMgr;
        //static ScriptManager﻿ scriptManager = new ScriptManager();

        /// <summary>
        /// 모든 이벤트 처리용
        /// </summary>
        public static void SetScenarioDataAll()
        {
            MyLog.LogDebug("ScenarioDataUtill.SetScenarioDataAll st"); 
            
            try
            {
                // 병렬 처리
                foreach (var scenarioData in GameMain.Instance.ScenarioSelectMgr.GetAllScenarioData())
                {
                // MyLog.LogMessageS(".SetScenarioDataAll:" + scenarioData.ID + " , " + scenarioData.ScenarioScript + " , " + scenarioData.IsPlayable + " , " + scenarioData.Title); ;
                if (scenarioData.IsPlayable)
                    {
                        bool b;
                        //MyLog.LogMessageS(".m_EventMaid");
                        foreach (var maid in scenarioData.GetEventMaidList())
                        {
                            b = maid.status.GetEventEndFlag(scenarioData.ID);
                            MyLog.LogMessage(".SetEventEndFlagAll:" + scenarioData.ID + " , " + scenarioData.ScenarioScript + " , " + maid.status.firstName + " , " + maid.status.lastName + " , " + b + " , " + scenarioData.ScenarioScript.Contains("_Marriage") + " , " + scenarioData.Title); ;
                            if (!b)
                            {
                                maid.status.SetEventEndFlag(scenarioData.ID, true);
                                if (scenarioData.ScenarioScript.Contains("_Marriage"))
                                {
                                    maid.status.specialRelation = SpecialRelation.Married;
                                    maid.status.relation = Relation.Lover;
                                    maid.status.OldStatus.isMarriage = true;
                                    maid.status.OldStatus.isNewWife = true;
                                    //SetMaidCondition(0, '嫁');
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MyLog.LogError("ScenarioDataUtill.SetScenarioDataAll : " + e.ToString());
            }

            return;
        }

        public static void SetScenarioFreeAll()
        {
            foreach (var data in GameMain.Instance.CharacterMgr.status.flags)
            {
                MyLog.LogMessage("flags"
                    , data.Key
                    , data.Value
                    );                
            }

            /*
            try
            {
                foreach (AbstractFreeModeItem data in GameUtill.scnearioFree)
                {
                    MyLog.LogMessage("scneario"
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
                }
            }
            catch (Exception e)
            {
                MyLog.LogError("scneario: " + e.ToString());
            }
            */
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
                MyLog.LogMessage(".RemoveEventEndFlagAll:" + stockMaid.status.firstName + " , " + stockMaid.status.lastName); ;
                action(stockMaid);
            }

        }

        public static void RemoveEventEndFlag(Maid maid)
        {
            if (maid!=null)
            {
                MyLog.LogMessage(".RemoveEventEndFlag:" + maid.status.firstName + " , " + maid.status.lastName); ;
                maid.status.RemoveEventEndFlagAll();
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
