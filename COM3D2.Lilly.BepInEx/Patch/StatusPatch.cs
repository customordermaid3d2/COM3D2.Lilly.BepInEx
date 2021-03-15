using HarmonyLib;
using MaidStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    class StatusPatch 
	{
		// Status

		[HarmonyPatch(typeof(Status), "GetFlag")]//,new Type[] { typeof(Personal.Data) }
		[HarmonyPostfix]
		public static void GetFlag( string flagName)//Status __instance,
		{
            //if (__instance ==null)
            {
				MyLog.LogMessage("GetFlag: " +  flagName);
				return;
            }
			//MyLog.LogMessage("GetFlag: " + MaidUtill.GetMaidFullNale( __instance.maid), flagName);
		}		

		[HarmonyPatch(typeof(Status), "AddFlag")]//,new Type[] { typeof(Personal.Data) }
		[HarmonyPostfix]
		public static void AddFlag( string flagName, int value)//Status __instance,
		{
			//if (__instance == null)
			{
				MyLog.LogMessage("AddFlag: " + flagName);
				return;
			}
			//MyLog.LogMessage("AddFlag: " + MaidUtill.GetMaidFullNale(__instance.maid), flagName, value);
		}

		[HarmonyPatch(typeof(Status), "RemoveFlag")]//,new Type[] { typeof(Personal.Data) }
		[HarmonyPostfix]
		public static void RemoveFlag(string flagName)//Status __instance, 
		{
			//if (__instance == null)
			{
				MyLog.LogMessage("RemoveFlag: " + flagName);
				return;
			}
			//MyLog.LogMessage("RemoveFlag: " + MaidUtill.GetMaidFullNale(__instance.maid), flagName);
		}

		//[HarmonyPatch(typeof(Status), "GetFlag")]//,new Type[] { typeof(Personal.Data) }
		//[HarmonyPatch(typeof(Status), "AddFlag")]//,new Type[] { typeof(Personal.Data) }
		//[HarmonyPatch(typeof(Status), "RemoveFlag")]//,new Type[] { typeof(Personal.Data) }
		//[HarmonyFinalizer]
		/*
		public static void Finalizer(Exception __exception)
		{
			MyLog.LogMessage("Status.Finalizer: " + __exception.ToString());
			__exception = null;
		}
		*/

		// public ReadOnlyDictionary<int, bool> eventEndFlags { get; private set; }
		// public ReadOnlyDictionary<string, int> flags { get; private set; }
		// public ReadOnlyDictionary<int, WorkData> workDatas { get; private set; }
		// public YotogiSkillSystem yotogiSkill { get; private set; }
		// public YotogiClassSystem yotogiClass { get; private set; }
		// public JobClassSystem jobClass { get; private set; }
		// public ReadOnlyDictionary<int, Propensity.Data> propensitys { get; private set; }
		// public Maid maid { get; private set; }

		//public static Status instance;

		// 도저히 안되겠음 인샙션뜸
		//[HarmonyPatch(typeof(Status), "SetPersonal",new Type[] { typeof(Personal.Data) })]
		//[HarmonyPostfix]
		//private static void SetPersonal(Status __instance, Personal.Data data) // string __m_BGMName 못가져옴
			/*
        {
            if (__instance != null)
            {
				instance = __instance;				
            }
            if (__instance.maid != null)
            {
				MyLog.LogMessage("SetPersonal:" + MaidUtill.GetMaidFullNale(__instance.maid));

            }
			*/

			// Personal.Data personal = __instance.personal;

			//return;
			/*
			if (!Personal.IsEnabled(data.id) && !data.oldPersonal)
			{
				Debug.LogError("性格[" + data.drawName + "]は有効ではないので設定できません");
				return false;
			}
			this.personal_ = data;
			this.additionalRelation = this.additionalRelation;
			if (this.personal_.oldPersonal)
			{
				this.heroineType = HeroineType.Transfer;
				if (this.OldStatus == null)
				{
					this.OldStatus = new Status(this);
				}
			}
			else if (this.heroineType == HeroineType.Transfer)
			{
				this.heroineType = HeroineType.Original;
				this.OldStatus = null;
			}
			return true;
		}
			*/


		/*

		private BonusStatus bonusStatus;
		public HeroineType heroineType;
		public JobClassSystem jobClass { get; private set; }

		/// <summary>
		/// 분석용
		/// </summary>
		public void UpdateClassBonusStatus()
		{
			this.bonusStatus.Clear();
			if (this.heroineType == HeroineType.Sub)
			{
				return;
			}
			foreach (KeyValuePair<int, ClassData<JobClass.Data>> keyValuePair in this.jobClass.GetAllDatas())
			{
				ClassData<JobClass.Data> value = keyValuePair.Value;
				value.data.levelBonuss[value.level - 1].ApplyAddBonusStatus(this.bonusStatus);
			}
		}

		*/
	}
}
