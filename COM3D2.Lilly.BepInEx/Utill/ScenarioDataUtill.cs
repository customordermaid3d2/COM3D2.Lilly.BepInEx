using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM3D2.Lilly.Plugin
{
    public static class ScenarioDataUtill
    {
        public  static void SetScenarioDataAll()
        {
            MyLog.LogMessageS("ScenarioDataUtill.SetScenarioDataAll");

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
            MyLog.LogMessageS(".m_EventMaid");
            foreach (var item in ___m_EventMaid)
            {
                b = item.status.GetEventEndFlag(__instance.ID);
                MyLog.LogMessageS(".m_EventMaid:" + item.status.firstName + " , " + item.status.lastName + " , " + b); ;
                if (b)
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
