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
            MyLog.LogInfoS("Application.installerName : " + Application.installerName);
            MyLog.LogInfoS("Application.version : " + Application.version);
            MyLog.LogInfoS("Application.unityVersion : " + Application.unityVersion);
            MyLog.LogInfoS("Application.companyName : " + Application.companyName);
            MyLog.LogInfoS("CharacterMgr.MaidStockMax : " + CharacterMgr.MaidStockMax);
            MyLog.LogInfoS("CharacterMgr.ActiveMaidSlotCount : " + CharacterMgr.ActiveMaidSlotCount);
            MyLog.LogInfoS("CharacterMgr.NpcMaidCreateCount : " + CharacterMgr.NpcMaidCreateCount);
            MyLog.LogInfoS("CharacterMgr.ActiveManSloatCount : " + CharacterMgr.ActiveManSloatCount);

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
            MyLog.LogInfoS("MaidStatusUtill.SetMaidStatus:name: " + ___select_maid_.status.firstName +" , "+ ___select_maid_.status.lastName);

            ___select_maid_.status.employmentDay = 1;// 고용기간

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
            
            ___select_maid_.status.studyRate = 0;   // 습득율
            ___select_maid_.status.likability = 999;// 호감도

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

        public static TJSScript tjs_;
        public static ScriptManager scriptManager;

        public static void Test()
        {
            scriptManager = GameMain.Instance.ScriptMgr;
            //GameMain.Instance
            //tjs_ = TJSScript.Create(scriptManager.file_system);
            //tjs_.AddFunction("IsYotogiClass", new TJSScript.FunctionCallBack(TJSFuncIsYotogiClass));
            //this.tjs_.AddFunction("IsMarriage", new TJSScript.FunctionCallBack(this.OldTJSFuncIsMarriage));
            //this.tjs_.AddFunction("IsNewWifeFlag", new TJSScript.FunctionCallBack(this.OldTJSFuncIsNewWifeFlag));

            //int nMaidNo = tjs_param[0].AsInteger();
            //Maid maid = GameMain.Instance.CharacterMgr.GetMaid(nMaidNo);
            //if (maid != null && maid.status.OldStatus != null)
            //{
            //    result.SetBool(maid.status.OldStatus.isMarriage);
            //}
            //else
            //{
            //    result.SetBool(false);
            //}
        }
    }
}
