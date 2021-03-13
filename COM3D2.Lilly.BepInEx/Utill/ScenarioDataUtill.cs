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
        /// <summary>
        /// 모든 이벤트 처리용
        /// </summary>
        public  static void SetScenarioDataAll()
        {
            MyLog.LogDebugS("ScenarioDataUtill.SetScenarioDataAll");

            // 병렬 처리
            Parallel.ForEach(GameMain.Instance.ScenarioSelectMgr.GetAllScenarioData(), scenarioData =>
            {
                MyLog.LogMessageS(".SetEventEndFlagAll:" + scenarioData.ID + " , " + scenarioData.IsPlayable + " , " + scenarioData.Title); ;
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
        /// <param name="__instance"></param>
        public static void SetEventEndFlagAll(List<Maid> ___m_EventMaid, ScenarioData __instance)
        {
            Action<Maid> action = delegate (Maid maid)
            {
                //maid.status.RemoveEventEndFlagAll();
                maid.status.SetEventEndFlag(__instance.ID, true);
            };
            bool b;
            //MyLog.LogMessageS(".m_EventMaid");
            foreach (var maid in ___m_EventMaid)
            {
                b = maid.status.GetEventEndFlag(__instance.ID);
                MyLog.LogMessageS(".SetEventEndFlagAll:" + __instance.ID +" , " + maid.status.firstName + " , " + maid.status.lastName + " , " + b + " , " + __instance.Title); ;
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
            CharacterMgr characterMgr = GameMain.Instance.CharacterMgr;
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
