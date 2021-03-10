using HarmonyLib;
using MaidStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    class StatusMgrPatch
    {
		[HarmonyPatch(typeof(StatusMgr), "UpdateMaidStatus")]
		[HarmonyPrefix]
		static void UpdateMaidStatusPre(Maid maid)
		{
            MyLog.LogMessageS("StatusMgr.UpdateMaidStatusPre"+ maid.status.charaName.name1 +" , "+ maid.status.charaName.name2);
		}

        [HarmonyPatch(typeof(StatusMgr), "LoadData")]
        [HarmonyPrefix]
        static bool LoadDataPre(ref Maid ___m_maid, StatusCtrl.Status __result)
        {
            MyLog.LogMessageS("StatusMgr.LoadDataPre");

			StatusCtrl.Status status = new StatusCtrl.Status();
			status.maidIcon = ___m_maid.GetThumIcon();
			NamePair charaName = ___m_maid.status.charaName;
			status.lastName = charaName.name1;
			status.firstName = charaName.name2;
			status.contractType = EnumConvert.GetString(___m_maid.status.contract);
			status.contractTypeTerm = EnumConvert.GetTerm(___m_maid.status.contract);
			status.personal = ___m_maid.status.personal.drawName;
			status.personalTerm = ___m_maid.status.personal.termName;
			status.sexualExperience = EnumConvert.GetString(___m_maid.status.seikeiken);
			status.sexualExperienceTerm = EnumConvert.GetTerm(___m_maid.status.seikeiken);
			ClassData<JobClass.Data> selectedJobClass = ___m_maid.status.selectedJobClass;
			if (selectedJobClass != null)
			{
				status.maidClassName = selectedJobClass.data.drawName;
				status.maidClassNameTerm = selectedJobClass.data.termName;
				status.maidClassLevel = selectedJobClass.level;
				status.maidClassExp = selectedJobClass.cur_exp;
				status.maidClassRequiredExp = selectedJobClass.next_exp;
			}
			ClassData<YotogiClass.Data> selectedYotogiClass = ___m_maid.status.selectedYotogiClass;
			if (selectedYotogiClass != null)
			{
				status.yotogiClassName = selectedYotogiClass.data.drawName;
				status.yotogiClassLevel = selectedYotogiClass.level;
				status.yotogiClassExp = selectedYotogiClass.cur_exp;
				status.yotogiClassRequiredExp = selectedYotogiClass.next_exp;
			}

			status.height = ___m_maid.status.body.height;
			status.weight = ___m_maid.status.body.weight;
			status.bust = ___m_maid.status.body.bust;
			status.cup = ___m_maid.status.body.cup;
			status.waist = ___m_maid.status.body.waist;
			status.hip = ___m_maid.status.body.hip;

			status.hp = ___m_maid.status.maxHp;
			status.mind = ___m_maid.status.maxMind;
			status.likability = ___m_maid.status.likability;
			status.care = ___m_maid.status.care;
			status.reception = ___m_maid.status.reception;
			status.cooking = ___m_maid.status.cooking;
			status.dance = ___m_maid.status.dance;
			status.vocal = ___m_maid.status.vocal;
			status.appeal = ___m_maid.status.maxAppealPoint;
			status.studyRate = ___m_maid.status.studyRate;
			status.teachRate = ___m_maid.status.teachRate;
			status.lovely = ___m_maid.status.lovely;
			status.elegance = ___m_maid.status.elegance;
			status.charm = ___m_maid.status.charm;
			status.inran = ___m_maid.status.inyoku;
			status.mValue = ___m_maid.status.mvalue;
			status.hentai = ___m_maid.status.hentai;
			status.housi = ___m_maid.status.housi;
			status.relation = EnumConvert.GetString(___m_maid.status.contract, ___m_maid.status.relation, ___m_maid.status.additionalRelation, ___m_maid.status.specialRelation);
			status.relationTerm = EnumConvert.GetTerm(___m_maid.status.contract, ___m_maid.status.relation, ___m_maid.status.additionalRelation, ___m_maid.status.specialRelation);
			if (___m_maid.status.OldStatus != null)
			{
				if (___m_maid.status.OldStatus.isMarriage)
				{
					status.relation = "嫁";
				}
				if (___m_maid.status.OldStatus.isNewWife)
				{
					status.relation = "新妻";
				}
				if (Product.isJapan)
				{
					status.relationTerm = string.Empty;
				}
			}
			status.conditionText = ___m_maid.status.conditionText;
			status.conditionTextTerm = ___m_maid.status.conditionTermText;
			status.yotogiPlayCount = ___m_maid.status.playCountYotogi;
			status.othersPlayCount = ___m_maid.status.playCountNightWork;
			status.ranking = ___m_maid.status.popularRank;
			status.acquisitionOfClientEvaluation = ___m_maid.status.totalEvaluations;
			status.acquisitionOfWorkingFunds = ___m_maid.status.totalSales;
			status.daysOfEmployment = ___m_maid.status.employmentElapsedDay;
			if (___m_maid.status.heroineType == HeroineType.Sub)
			{
				SubMaid.Data.CharacterStatus subCharaStatus = ___m_maid.status.subCharaStatus;
				status.contractType = subCharaStatus.contractText;
				status.personal = subCharaStatus.personalText;
				status.conditionText = subCharaStatus.relationText;
				status.contractTypeTerm = subCharaStatus.contractTextTerm;
				status.personalTerm = subCharaStatus.personalTextTerm;
				status.conditionTextTerm = subCharaStatus.relationTextTerm;
			}
			if (___m_maid.status.OldStatus != null)
			{
				status.mouth = ___m_maid.status.OldStatus.sexual.mouth;
				status.throat = ___m_maid.status.OldStatus.sexual.throat;
				status.nipple = ___m_maid.status.OldStatus.sexual.nipple;
				status.curi = ___m_maid.status.OldStatus.sexual.curi;
			}
			__result= status;
			return false;
		}
    }
}
