using MaidStatus;
using MaidStatus.CsvData;
using Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using wf;
using Yotogis;

namespace COM3D2.Lilly.Plugin
{

    static class MaidStatusUtill
    {
        // AddYotogiWorkResultParam 

        //static

        public static void SetMaidStatusAll()
        {
            MyLog.LogMessage("MaidStatusUtill.SetMaidStatusAll. start");

            ScheduleCSVData.vipFullOpenDay = 0;
            GameMain.Instance.CharacterMgr.status.clubGrade = 5;

            foreach (var maid in GameMain.Instance.CharacterMgr.GetStockMaidList())
            {
                SetMaidStatus(maid);
            }
            /*
            Parallel.ForEach(GameMain.Instance.CharacterMgr.GetStockMaidList(), maid =>
            {
                //MyLog.LogMessageS("MaidStatusUtill.ActiveManSloatCount: " + maid.status.firstName +" , "+ maid.status.lastName);

                SetMaidStatus(maid);
            });
            */
            MyLog.LogMessage("MaidStatusUtill.SetMaidStatusAll. end");
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
                    MyLog.LogMessage(".Skill1: " + MaidUtill.GetMaidFullNale(maid));
                    MyLog.LogDebug("id: " + data.id + " , " + data.name + " , " + data.start_call_file + " , " + data.start_call_file2 + " , " + data.termName);
                    MyLog.LogDebug("ban_id_array: " + MyUtill.Join<int>(" , ", data.ban_id_array));
                    MyLog.LogDebug("skill_exp_table: " + MyUtill.Join<int>(" , ", data.skill_exp_table));
                    MyLog.LogDebug("playable_stageid_list: " + MyUtill.Join<int>(" , ", data.playable_stageid_list));

                    YotogiSkillData yotogiSkillData = maid.status.yotogiSkill.Add(data);
                    SimpleExperienceSystem expSystem = yotogiSkillData.expSystem;
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
                foreach (Skill.Old.Data data in learnPossibleSkills)
                {
                    MyLog.LogMessage(".Skill2: " + MaidUtill.GetMaidFullNale(maid));
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
                MyLog.LogError("Skill2: " + MaidUtill.GetMaidFullNale(maid), e.ToString());
            }


            // 피드러 참고
            foreach (YotogiClass.Data data in YotogiClass.GetAllDatas(true))
            {
                ClassData<YotogiClass.Data> classData = maid.status.yotogiClass.Get(data.id) ?? maid.status.yotogiClass.Add(data, true, true);
                if ( classData != null)
                {
                    classData.expSystem.SetLevel(classData.expSystem.GetMaxLevel());
                }
            }

            //  피들러 참고 근데 사실상 똑같음
            /*
            foreach (JobClass.Data data in JobClass.GetAllDatas(true))
            {
                ClassData<JobClass.Data> classData = maid.status.jobClass.Get(data.id) ?? maid.status.jobClass.Add(data, true, true);
                if ( classData != null)
                {
                    classData.expSystem.SetLevel(classData.expSystem.GetMaxLevel());
                }
            }
            */

            // 실패한듯
            try
            {
                JobClassSystem jobClassSystem = maid.status.jobClass;

                List<JobClass.Data> learnPossibleClassDatas = jobClassSystem.GetLearnPossibleClassDatas(true, AbstractClassData.ClassType.Share | AbstractClassData.ClassType.New);
                // 클래스 추가?
                foreach (JobClass.Data data in learnPossibleClassDatas)
                {

                    if (jobClassSystem.Contains(data))
                        continue;

                    MyLog.LogMessage("JobClass.learn: " + MaidUtill.GetMaidFullNale(maid), learnPossibleClassDatas.Count);
                    MyLog.LogDebug("JobClass.learn:" + data.id + " , " + data.uniqueName + " , " + data.drawName + " , " + data.explanatoryText + " , " + data.termExplanatoryText);
                    MyLog.LogDebug("JobClass.learn: " + jobClassSystem.Contains(data), MyUtill.Join(" , ", data.levelBonuss));
                    ClassData<JobClass.Data> classData = jobClassSystem.Add(data, true, true);

                    //ClassData<JobClass.Data> classData=jobClassSystem.Get(data);
                    //SimpleExperienceSystem expSystem = classData.expSystem;
                    //expSystem.SetTotalExp(expSystem.GetMaxLevelNeedExp());
                    //expSystem.SetLevel(expSystem.GetMaxLevel());
                }

                SortedDictionary<int, ClassData<JobClass.Data>> keyValuePairs = jobClassSystem.GetAllDatas();
                //MyLog.LogMessage("JobClass.expSystem: " + MaidUtill.GetMaidFullNale(maid), keyValuePairs.Count);
                // 경험치 설정
                foreach (var item in keyValuePairs)
                {
                    ClassData<JobClass.Data> classData = item.Value;
                    JobClass.Data data = classData.data;
                    SimpleExperienceSystem expSystem = classData.expSystem;

                    if (expSystem.GetMaxLevel() == expSystem.GetCurrentLevel())
                        continue;

                    MyLog.LogDebug("JobClass.expSystem:" + data.id + " , " + data.uniqueName + " , " + data.drawName + " , " + data.explanatoryText + " , " + data.termExplanatoryText);
                    MyLog.LogDebug("JobClass.expSystem:" + expSystem.GetType() , expSystem.GetMaxLevel() , expSystem.GetCurrentLevel());

                    expSystem.SetTotalExp(expSystem.GetMaxLevelNeedExp());
                    expSystem.SetLevel(expSystem.GetMaxLevel());
                }
                //maid.status.UpdateClassBonusStatus();
            }
            catch (Exception e)
            {
                MyLog.LogError("JobClass: " + e.ToString());
            }


            return;

            // SceneFreeModeSelectManager.Start 참조

            /*
            if (this.mode == FreeModeSceneSelectBase.SelectMode.Story)
            {
                isEnabled = this.freemode_item_list_.SetList(FreeModeItemEveryday.CreateItemEverydayList(FreeModeItemEveryday.ScnearioType.Story, null).ToArray());
            }
            else if (this.mode == FreeModeSceneSelectBase.SelectMode.Everyday)
            {
                isEnabled = this.freemode_item_list_.SetList(FreeModeItemEveryday.CreateItemEverydayList(FreeModeItemEveryday.ScnearioType.Nitijyou, this.maid_.status).ToArray());
            }
            else if (this.mode == FreeModeSceneSelectBase.SelectMode.LifeMode)
            {
                isEnabled = this.freemode_item_list_.SetList(FreeModeItemLifeMode.CreateItemList(true).ToArray());
            }
            */

            /*
            try
            {
                foreach (FreeModeItemVip item in FreeModeItemVip.CreateItemVipList(maid.status))
                {

                }
            }
            catch (Exception e)
            {

                MyLog.LogError("JobClass: " + e.ToString());
            }


            try
            {
                // scene_label_everyday
                foreach (FreeModeItemEveryday item in FreeModeItemEveryday.CreateItemEverydayList(FreeModeItemEveryday.ScnearioType.Nitijyou, maid.status))
                {

                }
            }
            catch (Exception e)
            {

                MyLog.LogError("JobClass: " + e.ToString());
            }


            try
            {
                // scene_label_everyday
                foreach (FreeModeItemEveryday item in FreeModeItemEveryday.CreateItemEverydayList(FreeModeItemEveryday.ScnearioType.Nitijyou, maid.status))
                {

                }
            }
            catch (Exception e)
            {

                MyLog.LogError("JobClass: " + e.ToString());
            }



            // scene_label_mainstory
            List<FreeModeItemEveryday> list3 = FreeModeItemEveryday.CreateItemEverydayList(FreeModeItemEveryday.ScnearioType.Story, null);

            List<FreeModeItemLifeMode> list2 = FreeModeItemLifeMode.CreateItemList(true);

            */
        }


        public static void GetMaidStatus()
        {
            MyLog.LogMessage("ScenarioDataUtill.GetMaidStatus. start");

            Maid maid_ = GameMain.Instance.CharacterMgr.GetStockMaid(0);

            MyLog.LogMessage("Maid: " + MaidUtill.GetMaidFullNale(maid_));

            ReadOnlyDictionary<int, bool> eventEndFlags = maid_.status.eventEndFlags;
            foreach (var item in eventEndFlags)
            {
                MyLog.LogMessage("eventEndFlags: " + item.Key, item.Value);
            }

            ReadOnlyDictionary<string, int> flags = maid_.status.flags;
            foreach (var item in flags)
            {
                MyLog.LogMessage("flags: " + item.Key, item.Value);
            }

            ReadOnlyDictionary<int, WorkData> workDatas = maid_.status.workDatas;
            foreach (var item in workDatas)
            {
                MyLog.LogMessage("workDatas: " + item.Key, item.Value.id, item.Value.level);
            }

            MyLog.LogMessage("ScenarioDataUtill.GetMaidStatus. start");
        }
    }
}

