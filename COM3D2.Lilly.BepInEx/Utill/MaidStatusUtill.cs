using MaidStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Yotogis;

namespace COM3D2.Lilly.Plugin
{

    static class MaidStatusUtill
    {
        // AddYotogiWorkResultParam 

        public static void SetMaidStatusAll()
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
                //MyLog.LogMessageS("MaidStatusUtill.ActiveManSloatCount: " + maid.status.firstName +" , "+ maid.status.lastName);
                
                SetMaidStatus(maid);
            }
        }

        public static void SetMaidStatus(Maid ___select_maid_)
        {
            if (___select_maid_==null)
            {
                MyLog.LogErrorS("MaidStatusUtill.SetMaidStatus:null");
                return;
            }
            MyLog.LogMessageS("MaidStatusUtill.SetMaidStatus:name: " + ___select_maid_.status.firstName +" , "+ ___select_maid_.status.lastName);

            ___select_maid_.status.employmentDay = 1;

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
            
            ___select_maid_.status.studyRate = 0;

            try
            {
                foreach (Feature.Data data in Feature.GetAllDatas(true))
                {
                    ___select_maid_.status.AddFeature(data);
                }

                foreach (Propensity.Data data in Propensity.GetAllDatas(true))
                {
                    ___select_maid_.status.AddPropensity(data);
                }

                // 스킬 추가
                //___select_maid_.status.yotogiSkill.Add(skillId);
                {
                    List<Skill.Data> learnPossibleSkills = Skill.GetLearnPossibleSkills(___select_maid_.status);
                    foreach (Skill.Data data in learnPossibleSkills)
                    {
                        MyLog.LogMessageS("MaidStatusUtill.SetMaidStatus:Skill1: " + data.name + " , " + data.termName + " , " + data.start_call_file + " , " + data.start_call_file2);
                        ___select_maid_.status.yotogiSkill.Add(data.id);
                    }
                }
                {
                    List<Skill.Old.Data> learnPossibleSkills = Skill.Old.GetLearnPossibleSkills(___select_maid_.status);
                    foreach (Skill.Old.Data data in learnPossibleSkills)
                    {
                        MyLog.LogMessageS("MaidStatusUtill.SetMaidStatus:Skill2: " + data.name + " , " + data.start_call_file + " , " + data.start_call_file2);
                        ___select_maid_.status.yotogiSkill.Add(data.id);
                    }
                }
            }
            catch (Exception e)
            {
                MyLog.LogErrorS("MaidStatusUtill.SetMaidStatus: "+e.ToString());
            }
        }
    }
}
