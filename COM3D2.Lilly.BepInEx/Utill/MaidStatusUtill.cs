﻿using MaidStatus;
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

        public static void SetMaidStatus(Maid maid)
        {
            if (maid == null)
            {
                MyLog.LogErrorS("MaidStatusUtill.SetMaidStatus:null");
                return;
            }
            MyLog.LogInfoS(".SetMaidStatus:: " + MaidUtill.GetMaidFullNale(maid));

            maid.status.employmentDay = 1;// 고용기간

            maid.status.baseAppealPoint = 9999;
            maid.status.baseCare = 9999;
            maid.status.baseCharm = 9999;
            maid.status.baseCooking = 9999;
            maid.status.baseDance = 9999;
            maid.status.baseElegance = 9999;
            maid.status.baseHentai = 9999;
            maid.status.baseHousi = 9999;
            maid.status.baseInyoku = 9999;
            maid.status.baseLovely = 9999;
            maid.status.baseMaxHp = 9999;
            maid.status.baseMaxMind = 9999;
            maid.status.baseMaxReason = 9999;
            maid.status.baseMvalue = 9999;
            maid.status.baseReception = 9999;
            maid.status.baseTeachRate = 9999;
            maid.status.baseVocal = 9999;

            maid.status.studyRate = 0;   // 습득율
            maid.status.likability = 999;// 호감도

            maid.status.heroineType = HeroineType.Transfer;// 기본, ? , 이전
            maid.status.relation = Relation.Lover;// 호감도
            maid.status.seikeiken = Seikeiken.Yes_Yes;// 
            //___select_maid_.status.specialRelation = SpecialRelation.Married;// 호감도

            // this.tjs_.AddFunction("GetMaidStatus", new TJSScript.FunctionCallBack(this.TJSFuncGetMaidStatus));

            try
            {
                foreach (Feature.Data data in Feature.GetAllDatas(true))
                {
                    maid.status.AddFeature(data);
                }
            }
            catch (Exception e)
            {
                MyLog.LogErrorS("SetMaidStatus: " + e.ToString());
            }

            try
            {
                // 특성
                foreach (Propensity.Data data in Propensity.GetAllDatas(true))
                {
                    maid.status.AddPropensity(data);
                }
            }
            catch (Exception e)
            {
                MyLog.LogErrorS("SetMaidStatus: " + e.ToString());
            }

            // 스킬 추가
            //___select_maid_.status.yotogiSkill.Add(skillId);
            try
            {
                List<Skill.Data> learnPossibleSkills = Skill.GetLearnPossibleSkills(maid.status);
                foreach (Skill.Data data in learnPossibleSkills)
                {
                    MyLog.LogMessageS(".Skill1: " + MaidUtill.GetMaidFullNale(maid) );
                    MyLog.LogMessageS("id: " + data.id + " , " + data.name  + " , " + data.start_call_file + " , " + data.start_call_file2 + " , " + data.termName);
                    MyLog.LogMessageS("ban_id_array: " + MyUtill.Join<int>(" , ", data.ban_id_array));
                    MyLog.LogMessageS("skill_exp_table: " + MyUtill.Join<int>(" , ", data.skill_exp_table));                    
                    MyLog.LogMessageS("playable_stageid_list: " + MyUtill.Join<int>(" , ", data.playable_stageid_list));

                    maid.status.yotogiSkill.Add(data.id);
                }
            }
            catch (Exception e)
            {
                MyLog.LogErrorS("SetMaidStatus: " + e.ToString());
            }

            try
            {
                List<Skill.Old.Data> learnPossibleSkills = Skill.Old.GetLearnPossibleSkills(maid.status);
                foreach (Skill.Old.Data data in learnPossibleSkills)
                {
                    MyLog.LogMessageS(".Skill2: " + MaidUtill.GetMaidFullNale(maid) );
                    MyLog.LogMessageS("id: " + data.id + " , " + data.name + " , " + data.start_call_file + " , " + data.start_call_file2);
                    MyLog.LogMessageS("ban_id_array: " + MyUtill.Join(" , ", data.ban_id_array));
                    MyLog.LogMessageS("skill_exp_table: " + MyUtill.Join(" , ", data.skill_exp_table));

                    maid.status.yotogiSkill.Add(data.id);
                }
            }
            catch (Exception e)
            {
                MyLog.LogErrorS("SetMaidStatus: " + e.ToString());
            }

            try
            {
                // YotogiClass.CreateData();// 에서 이미 만들어짐. 두번 만들 필요 없음
                foreach (var item in maid.status.yotogiClass.GetAllDatas())
                {
                    //base.maid.status.selectedYotogiClass.expSystem.GetMaxLevel()
                    MyLog.LogMessageS(".Class: " + MaidUtill.GetMaidFullNale(maid) + " , " + item.Value.data.id + " , " + item.Value.data.drawName + " , " + item.Value.data.learnConditions + " , " + item.Value.data.termExplanatoryText+ " , " + item.Value.data.termName+ " , " + item.Value.data.uniqueName);
                    
                }
            }
            catch (Exception e)
            {
                MyLog.LogErrorS("SetMaidStatus: " + e.ToString());
            }

        }

        //public static TJSScript tjs_;
        public static ScriptManager scriptManager = new ScriptManager();
        public static ScenarioSelectMgr scenarioSelectMgr;
        public static ScenarioData[] scenarioDatas;

        public static void Test()
        {
            //scriptManager = GameMain.Instance.ScriptMgr;
            scenarioSelectMgr = GameMain.Instance.ScenarioSelectMgr;
            scenarioDatas = scenarioSelectMgr.GetAllScenarioData();

            foreach (var scenarioData in scenarioDatas)
            {
                string text﻿ = scenarioData.ScenarioScript;
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
