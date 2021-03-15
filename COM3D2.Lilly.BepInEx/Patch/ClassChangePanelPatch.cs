using HarmonyLib;
using MaidStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yotogis;

namespace COM3D2.Lilly.Plugin
{
    class ClassChangePanelPatch
    {
        public static Dictionary<int, SortedList<int, List<Skill.Data>>> job_class_special_skill_list_;// = new Dictionary<int, SortedList<int, List<Skill.Data>>>();



        [HarmonyPatch(typeof(ClassChangePanel), "CreateClassText")]
        [HarmonyPostfix]
        public static void CreateClassText(Dictionary<int, SortedList<int, List<Skill.Data>>> ___job_class_special_skill_list_)
        {
            //return;
            job_class_special_skill_list_ = ___job_class_special_skill_list_;
            return;

            if (job_class_special_skill_list_.Count != 0)
            {
                return;
            }
            foreach (JobClass.Data data in JobClass.GetAllDatas(false))
            {
                job_class_special_skill_list_.Add(data.id, new SortedList<int, List<Skill.Data>>());
            }
        }

        public static Maid maid_;
        public static Dictionary<int, ClassUnit> job_class_unit_dic_ = new Dictionary<int, ClassUnit>();
        public static Dictionary<int, ClassUnit> yotogi_class_unit_dic_ = new Dictionary<int, ClassUnit>();

        // public void SetTargetMaid(Maid maid)
        [HarmonyPatch(typeof(ClassChangePanel), "SetTargetMaid")]
        [HarmonyPostfix]
        public static void SetTargetMaid(Maid maid, Dictionary<int, ClassUnit> ___job_class_unit_dic_, Dictionary<int, ClassUnit> ___yotogi_class_unit_dic_)
        {
            maid_ = maid;
            job_class_unit_dic_ = ___job_class_unit_dic_;
            yotogi_class_unit_dic_ = ___yotogi_class_unit_dic_;

            foreach (KeyValuePair<int, ClassUnit> item in job_class_unit_dic_)
            {
                ClassUnit value = item.Value;
                JobClass.Data data = JobClass.GetData(value.maid_class_type);
                MyLog.LogMessageS(data.id, data.classType, data.drawName, data.uniqueName, data.explanatoryText, data.termName, data.termExplanatoryText);

            }
            foreach (KeyValuePair<int, ClassUnit> item in yotogi_class_unit_dic_)
            {
                ClassUnit value = item.Value;
                YotogiClass.Data data = YotogiClass.GetData(value.yotogi_class_type);
                MyLog.LogMessageS(data.id, data.classType, data.drawName, data.uniqueName, data.explanatoryText, data.termName, data.termExplanatoryText);
            }

            return;
            /*
            if (this.job_class_unit_dic_.Count == 0)
            {
                bool activeSelf = base.gameObject.activeSelf;
                base.gameObject.SetActive(true);
                this.Awake();
                base.gameObject.SetActive(activeSelf);
            }
            this.maid_ = maid;
            Status status = this.maid_.status;
            foreach (KeyValuePair<int, ClassUnit> keyValuePair in this.job_class_unit_dic_)
            {
                keyValuePair.Value.UpdateMaidData(this.maid_);
            }
            this.jobUIs.tabPanel.ResetSelect();
            foreach (KeyValuePair<int, ClassUnit> keyValuePair2 in this.job_class_unit_dic_)
            {
                ClassUnit value = keyValuePair2.Value;
                value.button.SetSelect(false);
                value.button.isEnabled = status.jobClass.Contains(value.maid_class_type);
                JobClass.Data data = JobClass.GetData(value.maid_class_type);
                if (MaidManagement.compatibilityMode)
                {
                    value.gameObject.SetActive(data.classType != AbstractClassData.ClassType.New);
                }
                else
                {
                    value.gameObject.SetActive(data.classType != AbstractClassData.ClassType.Old);
                }
                if (value.gameObject.activeSelf)
                {
                    value.gameObject.SetActive(data.learnConditions.isLearnPossiblePersonal(maid.status.personal));
                }
            }
            GameObject gameObject = this.yotogiUIs.itemGrid.gameObject;
            while (gameObject.transform.parent != null && !gameObject.transform.gameObject.activeInHierarchy)
            {
                gameObject = ((!(gameObject.transform.parent != null)) ? null : gameObject.transform.parent.gameObject);
            }
            if (gameObject != null)
            {
                gameObject.SetActive(true);
            }
            this.jobUIs.Reset();
            int num = (status.selectedJobClass == null) ? int.MinValue : status.selectedJobClass.data.id;
            if (num != -2147483648)
            {
                this.jobUIs.tabPanel.Select(this.job_class_unit_dic_[num].button);
                if (this.jobUIs.classListObject.activeSelf)
                {
                    this.job_class_unit_dic_[num].UpdateInfo();
                }
            }
            foreach (KeyValuePair<int, ClassUnit> keyValuePair3 in this.yotogi_class_unit_dic_)
            {
                keyValuePair3.Value.UpdateMaidData(this.maid_);
            }
            this.yotogiUIs.tabPanel.ResetSelect();
            foreach (KeyValuePair<int, ClassUnit> keyValuePair4 in this.yotogi_class_unit_dic_)
            {
                ClassUnit value2 = keyValuePair4.Value;
                value2.button.SetSelect(false);
                value2.button.isEnabled = status.yotogiClass.Contains(value2.yotogi_class_type);
                YotogiClass.Data data2 = YotogiClass.GetData(value2.yotogi_class_type);
                value2.gameObject.SetActive(data2.learnConditions.isLearnPossiblePersonal(maid.status.personal));
            }
            gameObject = this.yotogiUIs.itemGrid.gameObject;
            while (gameObject.transform.parent != null && !gameObject.transform.gameObject.activeInHierarchy)
            {
                gameObject = ((!(gameObject.transform.parent != null)) ? null : gameObject.transform.parent.gameObject);
            }
            if (gameObject != null)
            {
                gameObject.SetActive(true);
            }
            this.yotogiUIs.Reset();
            num = ((status.selectedYotogiClass == null) ? int.MinValue : status.selectedYotogiClass.data.id);
            if (num != -2147483648)
            {
                this.yotogiUIs.tabPanel.Select(this.yotogi_class_unit_dic_[num].button);
                if (this.yotogiUIs.classListObject.activeSelf)
                {
                    this.yotogi_class_unit_dic_[num].UpdateInfo();
                }
            }
            if (this.class_type_tab_panel_.GetSelectButtonObject() == null)
            {
                this.class_type_tab_panel_.Select(this.job_class_button_);
            }

            */
        }
    }
}
