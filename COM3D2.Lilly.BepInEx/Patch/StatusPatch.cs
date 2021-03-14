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

		// public ReadOnlyDictionary<int, bool> eventEndFlags { get; private set; }
		// public ReadOnlyDictionary<string, int> flags { get; private set; }
		// public ReadOnlyDictionary<int, WorkData> workDatas { get; private set; }
		// public YotogiSkillSystem yotogiSkill { get; private set; }
		// public YotogiClassSystem yotogiClass { get; private set; }
		// public JobClassSystem jobClass { get; private set; }
		// public ReadOnlyDictionary<int, Propensity.Data> propensitys { get; private set; }
		// public Maid maid { get; private set; }

		static Status instance;

		// 도저히 안되겠음 인샙션뜸
		//[HarmonyPatch(typeof(Status), "SetPersonal",new Type[] { typeof(Personal.Data) })]
        //[HarmonyPostfix]
        private static void SetPersonal(Status __instance, Personal.Data data) // string __m_BGMName 못가져옴
        {
            if (__instance != null)
            {
				instance = __instance;				
            }
            if (__instance.maid != null)
            {
				MyLog.LogMessageS("SetPersonal:" + MaidUtill.GetMaidFullNale(__instance.maid));

            }


			// Personal.Data personal = __instance.personal;

			return;
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
			*/
		}
    }
}
