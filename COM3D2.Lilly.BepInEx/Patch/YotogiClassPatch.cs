using HarmonyLib;
using MaidStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wf;
using Yotogis;

namespace COM3D2.Lilly.Plugin.Patch
{
    class YotogiClassPatch
    {
        static CsvCommonIdManager commonIdManager;
        static Dictionary<int, YotogiClass.Data> basicDatas;
        static Dictionary<int, SortedList<int, List<Skill.Data>>> learnSkillList;
        static Dictionary<int, SortedList<int, List<Skill.Old.Data>>> learnSkillOldList;

        [HarmonyPatch(typeof(YotogiClass), "CreateData")]
        [HarmonyPostfix]
        public static void CreateData(CsvCommonIdManager ___commonIdManager , 
            Dictionary<int, YotogiClass.Data> ___basicDatas, 
            Dictionary<int, SortedList<int, List<Skill.Data>>> ___learnSkillList, 
            Dictionary<int, SortedList<int, List<Skill.Old.Data>>> ___learnSkillOldList)
        {
            if (commonIdManager != null)
            {
                return;
            }
            MyLog.LogMessage("YotogiClass.CreateData()");
            commonIdManager = ___commonIdManager;
            basicDatas = ___basicDatas;
            learnSkillList = ___learnSkillList;
            learnSkillOldList = ___learnSkillOldList;
        }
        // yotogi_class_enabled_list.nei
        // string text = "maid_status_yotogiclass_" + array[i] + ".nei";
        string[] array = new string[]
        {
                "list",
                "acquired_condition",
                "bonus",
                "experiences"
        };


        public static SortedList<int, List<Skill.Data>> GetLearnSkillList(int id)
        {
            if (learnSkillList == null)
            {
                learnSkillList = new Dictionary<int, SortedList<int, List<Skill.Data>>>();
                Skill.CreateData();
                YotogiClass.CreateData();
                foreach (YotogiClass.Data data in YotogiClass.GetAllDatas(false))
                {
                    learnSkillList.Add(data.id, new SortedList<int, List<Skill.Data>>());
                }
                for (int i = 0; i < Skill.skill_data_list.Length; i++)
                {
                    foreach (Skill.Data data2 in Skill.skill_data_list[i].Values)
                    {
                        if (data2.getcondition_data.yotogi_class != null)
                        {
                            if (learnSkillList.ContainsKey(data2.getcondition_data.yotogi_class.id))
                            {
                                SortedList<int, List<Skill.Data>> sortedList = learnSkillList[data2.getcondition_data.yotogi_class.id];
                                if (sortedList.ContainsKey(data2.getcondition_data.yotogi_class_level))
                                {
                                    sortedList[data2.getcondition_data.yotogi_class_level].Add(data2);
                                }
                                else
                                {
                                    List<Skill.Data> list = new List<Skill.Data>();
                                    list.Add(data2);
                                    sortedList.Add(data2.getcondition_data.yotogi_class_level, list);
                                }
                            }
                        }
                    }
                }
            }
            NDebug.Assert(learnSkillList.ContainsKey(id), "夜伽クラス\nID[" + id + "]のデータは存在しません");
            return learnSkillList[id];
        }



    }
}
