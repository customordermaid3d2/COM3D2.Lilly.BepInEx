using MaidStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Yotogis;

namespace COM3D2.Lilly.Plugin
{

    static class MaidStatusUtill
    {
        // AddYotogiWorkResultParam 

        //static

        public static void SetMaidStatusAll()
        {
            MyLog.LogDebugS("MaidStatusAll ");

            Parallel.ForEach(GameMain.Instance.CharacterMgr.GetStockMaidList(), maid =>
            {
                //MyLog.LogMessageS("MaidStatusUtill.ActiveManSloatCount: " + maid.status.firstName +" , "+ maid.status.lastName);
                
                SetMaidStatus(maid);
            });
        }

        public static void SetMaidStatus(Maid ___select_maid_)
        {
            if (___select_maid_==null)
            {
                MyLog.LogErrorS("MaidStatusUtill.SetMaidStatus:null");
                return;
            }
            MyLog.LogInfoS(".SetMaidStatus:: " + MaidUtill.GetMaidFullNale(___select_maid_));

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

            ___select_maid_.status.heroineType=HeroineType.Transfer;// 기본, ? , 이전
            ___select_maid_.status.relation= Relation.Lover;// 호감도
            ___select_maid_.status.seikeiken = Seikeiken.Yes_Yes;// 
            //___select_maid_.status.specialRelation = SpecialRelation.Married;// 호감도

            // this.tjs_.AddFunction("GetMaidStatus", new TJSScript.FunctionCallBack(this.TJSFuncGetMaidStatus));



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
                        MyLog.LogMessageS(".:Skill1: " + MaidUtill.GetMaidFullNale(___select_maid_) + " , " + data.name + " , " + data.termName + " , " + data.start_call_file + " , " + data.start_call_file2);
                        ___select_maid_.status.yotogiSkill.Add(data.id);
                    }
                }
                {
                    List<Skill.Old.Data> learnPossibleSkills = Skill.Old.GetLearnPossibleSkills(___select_maid_.status);
                    foreach (Skill.Old.Data data in learnPossibleSkills)
                    {
                        MyLog.LogMessageS(".:Skill2: " + MaidUtill.GetMaidFullNale(___select_maid_) + " , " + data.name + " , " + data.start_call_file + " , " + data.start_call_file2);
                        ___select_maid_.status.yotogiSkill.Add(data.id);
                    }
                }
            }
            catch (Exception e)
            {
                MyLog.LogErrorS("MaidStatusUtill.SetMaidStatus: "+e.ToString());
            }
        }

        //public static TJSScript tjs_;
        public static ScriptManager scriptManager= new ScriptManager();
        public static ScenarioSelectMgr scenarioSelectMgr;
        public static ScenarioData[] scenarioDatas;

        public static void Test()
        {
            //scriptManager = GameMain.Instance.ScriptMgr;
            scenarioSelectMgr = GameMain.Instance.ScenarioSelectMgr;
            scenarioDatas = scenarioSelectMgr.GetAllScenarioData();

            foreach (var scenarioData in scenarioDatas)
            {
                string text﻿=scenarioData.ScenarioScript;
                //if (this.m_SelectedMaid.Count > 0 && text.IndexOf("?") >= 0)
                //{
                //    text = ScriptManager.ReplacePersonal(this.m_SelectedMaid[0], text);
                //}
                ////ScriptManager
                //GameMain.Instance.ScriptMgr.EvalScript("&tf['scenario_file_name'] = '" + text + "';");
                //GameMain.Instance.ScriptMgr.EvalScript("&tf['label_name'] = '" + this.m_CurrentScenario.ScriptLabel + "';");
                
                scriptManager.EvalScript("&tf['scenario_file_name'] = '" + text + "';");

                // ScriptManager.EvalScriptPost2:&tf['scenario_file_name'] = 'j1_marriage_0006';
                // [Message:     Lilly] ScriptManager.EvalScriptPost2:&tf['label_name'] = '*OK押された時のとび先';

                //ScriptManager.EvalScript(string) : void @06004DF7
                //public void EvalScript(string eval_str)
                //{
                //    this.tjs_.EvalScript(eval_str);
                //}

                // this.tjs_ = TJSScript.Create(this.file_system);
                // ScriptManager.tjs_ : TJSScript @04004686
                // this.tjs_.AddFunction("IsMarriage", new TJSScript.FunctionCallBack(this.OldTJSFuncIsMarriage));
                // ﻿public delegate void FunctionCallBack(TJSVariantRef[] param, TJSVariantRef result);﻿


            }

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
