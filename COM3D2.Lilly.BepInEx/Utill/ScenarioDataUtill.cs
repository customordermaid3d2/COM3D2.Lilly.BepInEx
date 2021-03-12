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

            Parallel.ForEach(GameMain.Instance.ScenarioSelectMgr.GetAllScenarioData(), scenarioData =>
            {
                if (scenarioData.IsPlayable)
                {                    
                    SetEventEndFlagAll(scenarioData.GetEventMaidList(), scenarioData);
                }
            });
        }


        public static void SetEventEndFlagAll(List<Maid> ___m_EventMaid, ScenarioData __instance)
        {
            bool b;
            //MyLog.LogMessageS(".m_EventMaid");
            foreach (var item in ___m_EventMaid)
            {
                b = item.status.GetEventEndFlag(__instance.ID);
                MyLog.LogMessageS(".SetEventEndFlagAll:" + __instance.ID +" , " + item.status.firstName + " , " + item.status.lastName + " , " + b + " , " + __instance.Title); ;
                if (!b)
                {
                    item.status.SetEventEndFlag(__instance.ID, true);
                }
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
