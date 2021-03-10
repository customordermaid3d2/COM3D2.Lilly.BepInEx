using MaidStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin
{

    static class MaidStatus
    {
        // AddYotogiWorkResultParam 

        public static void MaidStatusAll()
        {
            MyLog.LogDebugS("MaidStatusAll ");
            MyLog.LogMessageS("Application.installerName : " + Application.installerName);
            MyLog.LogMessageS("Application.version : " + Application.version);
            MyLog.LogMessageS("Application.unityVersion : " + Application.unityVersion);
            MyLog.LogMessageS("Application.companyName : " + Application.companyName);
            MyLog.LogMessageS("CharacterMgr.MaidStockMax : " + CharacterMgr.MaidStockMax);
            MyLog.LogMessageS("CharacterMgr.ActiveMaidSlotCount : " + CharacterMgr.ActiveMaidSlotCount);
            MyLog.LogMessageS("CharacterMgr.NpcMaidCreateCount : " + CharacterMgr.NpcMaidCreateCount);
            MyLog.LogMessageS("CharacterMgr.ActiveManSloatCount : " + CharacterMgr.ActiveManSloatCount);

            foreach (Maid maid in GameMain.Instance.CharacterMgr.GetStockMaidList())
            {
                MyLog.LogMessageS("CharacterMgr.ActiveManSloatCount: " + maid.status.firstName +" , "+ maid.status.lastName);
                
                SetMaidStatus(maid);
            }
        }


        public static void SetMaidStatus(Maid ___select_maid_)
        {
            ___select_maid_.status.baseAppealPoint = 9999;
            ___select_maid_.status.baseCare = 9999;
            ___select_maid_.status.baseCharm = 9999;
            ___select_maid_.status.baseCooking = 9999;
            ___select_maid_.status.baseDance = 9999;
            ___select_maid_.status.baseElegance = 9999;
            ___select_maid_.status.baseHentai = 9999;
            ___select_maid_.status.baseHousi = 9999;
            ___select_maid_.status.baseInyoku = 9999;
            ___select_maid_.status.baseLovely = 9999;
            ___select_maid_.status.baseMaxHp = 9999;
            ___select_maid_.status.baseMaxMind = 9999;
            ___select_maid_.status.baseMaxReason = 9999;
            ___select_maid_.status.baseMvalue = 9999;
            ___select_maid_.status.baseReception = 9999;
            ___select_maid_.status.baseTeachRate = 9999;
            ___select_maid_.status.baseVocal = 9999;
        }
    }
}
