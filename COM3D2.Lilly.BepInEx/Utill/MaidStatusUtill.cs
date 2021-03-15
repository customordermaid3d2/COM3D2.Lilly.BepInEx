using MaidStatus;
using MaidStatus.CsvData;
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
            MyLog.LogDebug("MaidStatusAll ");

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
                MyLog.LogError("MaidStatusUtill.SetMaidStatus:null");
                return;
            }
            MyLog.LogMessage(".SetMaidStatus:: " + MaidUtill.GetMaidFullNale(maid));

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
                MyLog.LogError("SetMaidStatus: " + e.ToString());
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
                MyLog.LogError("SetMaidStatus: " + e.ToString());
            }

            // 스킬 추가
            //___select_maid_.status.yotogiSkill.Add(skillId);
            try
            {
                List<Skill.Data> learnPossibleSkills = Skill.GetLearnPossibleSkills(maid.status);
                foreach (Skill.Data data in learnPossibleSkills)
                {
                    MyLog.LogDebug(".Skill1: " + MaidUtill.GetMaidFullNale(maid) );
                    MyLog.LogDebug("id: " + data.id + " , " + data.name  + " , " + data.start_call_file + " , " + data.start_call_file2 + " , " + data.termName);
                    MyLog.LogDebug("ban_id_array: " + MyUtill.Join<int>(" , ", data.ban_id_array));
                    MyLog.LogDebug("skill_exp_table: " + MyUtill.Join<int>(" , ", data.skill_exp_table));                    
                    MyLog.LogDebug("playable_stageid_list: " + MyUtill.Join<int>(" , ", data.playable_stageid_list));

                    YotogiSkillData yotogiSkillData = maid.status.yotogiSkill.Add(data);
                    SimpleExperienceSystem expSystem =yotogiSkillData.expSystem;
                    expSystem.SetTotalExp(expSystem.GetMaxLevelNeedExp());
                    expSystem.SetLevel(expSystem.GetMaxLevel());
                }
            }
            catch (Exception e)
            {
                MyLog.LogError("Skill1: " + e.ToString());
            }

            try
            {
                List<Skill.Old.Data> learnPossibleSkills = Skill.Old.GetLearnPossibleSkills(maid.status);
                MyLog.LogMessage(".Skill2: " + MaidUtill.GetMaidFullNale(maid) );
                foreach (Skill.Old.Data data in learnPossibleSkills)
                {
                    MyLog.LogDebug("id: " + data.id + " , " + data.name + " , " + data.start_call_file + " , " + data.start_call_file2);
                    MyLog.LogDebug("ban_id_array: " + MyUtill.Join(" , ", data.ban_id_array));
                    MyLog.LogDebug("skill_exp_table: " + MyUtill.Join(" , ", data.skill_exp_table));

                    YotogiSkillData yotogiSkillData = maid.status.yotogiSkill.Add(data);
                    SimpleExperienceSystem expSystem = yotogiSkillData.expSystem;
                    expSystem.SetTotalExp(expSystem.GetMaxLevelNeedExp());
                    expSystem.SetLevel(expSystem.GetMaxLevel());
                }
            }
            catch (Exception e)
            {
                MyLog.LogError("Skill2: " + e.ToString());
            }

            // 실패한듯
            try
            {
                JobClassSystem jobClassSystem = maid.status.jobClass;

                List<JobClass.Data> learnPossibleClassDatas = jobClassSystem.GetLearnPossibleClassDatas(true, AbstractClassData.ClassType.Share | AbstractClassData.ClassType.New);
                MyLog.LogMessage("JobClass.learn: " + MaidUtill.GetMaidFullNale(maid), learnPossibleClassDatas.Count);
                
                foreach (JobClass.Data data in learnPossibleClassDatas)
                {
                    MyLog.LogDebug("JobClass.learn:" + data.id + " , " + data.uniqueName+ " , " + data.drawName + " , " + data.explanatoryText + " , " + data.termExplanatoryText);
                    MyLog.LogDebug("JobClass.learn: " + jobClassSystem.Contains(data) , MyUtill.Join(" , ", data.levelBonuss));
                    if (!jobClassSystem.Contains(data))
                    {
                        ClassData<JobClass.Data> classData=jobClassSystem.Add(data, true, true);
                    } 
                    //ClassData<JobClass.Data> classData=jobClassSystem.Get(data);
                    //SimpleExperienceSystem expSystem = classData.expSystem;
                    //expSystem.SetTotalExp(expSystem.GetMaxLevelNeedExp());
                    //expSystem.SetLevel(expSystem.GetMaxLevel());
                }

                SortedDictionary<int, ClassData<JobClass.Data>> keyValuePairs =jobClassSystem.GetAllDatas();
                MyLog.LogMessage("JobClass.expSystem: " + MaidUtill.GetMaidFullNale(maid) , keyValuePairs.Count);
                
                foreach (var item in keyValuePairs)
                {
                    ClassData<JobClass.Data> classData = item.Value;
                    JobClass.Data data = classData.data;

                    MyLog.LogDebug("JobClass.expSystem:" + data.id + " , " + data.uniqueName + " , " + data.drawName + " , " + data.explanatoryText + " , " + data.termExplanatoryText);

                    SimpleExperienceSystem expSystem = classData.expSystem;
                    expSystem.SetTotalExp(expSystem.GetMaxLevelNeedExp());
                    expSystem.SetLevel(expSystem.GetMaxLevel());
                }
                //maid.status.UpdateClassBonusStatus();
            }
            catch (Exception e)
            {
                MyLog.LogError("JobClass: " + e.ToString());
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
